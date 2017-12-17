namespace IEnumerableTrie
{
    public interface IHasStringKeys
    {
        string[] Keys { get; }

        public static implicit operator IHasStringKeys(string str)
        {
            return d.val;
        }
    }
}