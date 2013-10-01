using Gecko.Collections;

namespace Gecko.Windows
{
    public sealed class JumpListBuilder
    {
        private readonly nsIJumpListBuilder _jumpListBuilder;

        internal JumpListBuilder(nsIJumpListBuilder jumpListBuilder)
        {
            _jumpListBuilder = jumpListBuilder;
        }

        public short Available
        {
            get { return _jumpListBuilder.GetAvailableAttribute(); }
        }

        public bool IsListCommitted
        {
            get { return _jumpListBuilder.GetIsListCommittedAttribute(); }
        }

        public short tMaxListItems
        {
            get { return _jumpListBuilder.GetMaxListItemsAttribute(); }
        }

        public void AbortListBuild()
        {

            _jumpListBuilder.AbortListBuild();
        }

        public bool AddListToBuild(short aCatType, JumpListItem[] items, string catName)
        {
            var array = GeckoCollectionsHelper.CreateArray();
#warning TODO
            for (int i = 0; i < items.Length; i++)
            {
                //array.AppendElement(items[i]._item, false);
            }

            return nsString.Pass(x => _jumpListBuilder.AddListToBuild(aCatType, array, x), catName);
        }

        public bool CommitListBuild()
        {

            return _jumpListBuilder.CommitListBuild();
        }

        public bool DeleteActiveList()
        {

            return _jumpListBuilder.DeleteActiveList();
        }

        public bool InitListBuild(out JumpListItem[] removedItems)
        {
            var array = Collections.GeckoCollectionsHelper.CreateArray();
            bool ret = _jumpListBuilder.InitListBuild(array);
            removedItems = null;
            if (ret)
            {
                removedItems = new JumpListItem[array.GetLengthAttribute()];
                for (int i = 0; i < removedItems.Length; i++)
                {

                    removedItems[i] = new JumpListItem(this.GetElementAs<nsIJumpListItem>(array, i));
                    //array.GetElementAs<nsIJumpListItem>( i ) );
                }
            }
            return ret;
        }

        private nsIJumpListItem GetElementAs<T1>(nsIMutableArray array, int index)
        {
            var enumerator = array.Enumerate();
            int j = -1;
            while (enumerator.HasMoreElements())
            {
                j++;
                var one = enumerator.GetNext();
                if (j == index)
                {
                    return (nsIJumpListItem)one;
                }
            }

            return default(nsIJumpListItem);
        }

    }
}