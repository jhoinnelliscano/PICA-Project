namespace eCommerce.Commons.Utilities.Node
{
    public interface INode<T> where T : new()        
    {
        public T Parent { get; set; }
        public IEnumerable<INode<T>> ChildNodes { get; set; }

    }
}
