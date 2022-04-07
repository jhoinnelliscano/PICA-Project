using eCommerce.Commons.Utilities.Node;

namespace eCommerce.Commons.Objects.WebModels
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Category { get; set; } = null!;
        public bool Status { get; set; }
    }

    public class TreeNodeCategoryModel : Node<CategoryModel>
    {

    }
}
