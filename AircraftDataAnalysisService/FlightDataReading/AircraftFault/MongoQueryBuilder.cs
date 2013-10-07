using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataReading.AircraftFault
{
    public class MongoQueryBuilder
    {
        public static IMongoQuery BuildQuery(ParamCondition condition)
        {
            if (condition == null)
                return null;

            if (condition.Conditions != null && condition.Conditions.Length > 0)
            {//build condition tree recursive
                if (condition.Relation == ConditionRelation.OR)
                {
                    var subConds = from one in condition.Conditions
                                   select BuildQueryOneLevel(one);
                    IMongoQuery query = Query.Or(subConds);

                    return query;
                }
                else
                {
                    var subConds = from one in condition.Conditions
                                   select BuildQueryOneLevel(one);
                    IMongoQuery query = Query.And(subConds);

                    return query;
                }
            }
            else
            {//只默认AND运算符
                IMongoQuery subQuery = BuildQueryOneLevel(condition);
                return subQuery;
            }
        }

        private static IMongoQuery BuildQueryOneLevel(ParamCondition condition)
        {
            if (condition == null)
                return null;

            if (condition.Conditions != null && condition.Conditions.Length > 0)
                return BuildQuery(condition);

            switch (condition.Operator)
            {
                case Operator.Equal:
                    return Query.EQ(condition.Parameter.ParameterID,
                        GetParameterValue(condition.Parameter, condition.Value));
                case Operator.GreaterOrEqual:
                    return Query.GTE(condition.Parameter.ParameterID,
                        GetParameterValue(condition.Parameter, condition.Value));
                case Operator.NotEqual:
                    return Query.NE(condition.Parameter.ParameterID,
                        GetParameterValue(condition.Parameter, condition.Value));
                case Operator.GreaterThan:
                    return Query.GT(condition.Parameter.ParameterID,
                        GetParameterValue(condition.Parameter, condition.Value));
                case Operator.SmallerOrEqual:
                    return Query.LTE(condition.Parameter.ParameterID,
                        GetParameterValue(condition.Parameter, condition.Value));
                case Operator.SmallerThan:
                    return Query.LT(condition.Parameter.ParameterID,
                        GetParameterValue(condition.Parameter, condition.Value));
                case Operator.TimeRange: //
                    break;
            }
            return null;
        }

        private static BsonValue GetParameterValue(FlightDataEntities.FlightParameter flightParameter, string value)
        {
            return DataTypeConverter.ToBsonType(value, flightParameter.ParameterDataType);
        }
    }
}
