
namespace eCommerce.Commons.Utilities.Node
{
    public class Node<T>: INode<T> where T : new()
    {
        public Node()
        {

        }
        public T Parent { get; set; }
        public IEnumerable<INode<T>> ChildNodes { get; set; }        
    }
}
