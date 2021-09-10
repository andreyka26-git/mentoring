namespace Tasks
{
    public class Node<T>
    {
        public Node<T> Prev { get; set; }
        public Node<T> Next { get; set; }
        public T Value { get; set; }
    }
}