namespace InStudio.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SortableColumnAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
