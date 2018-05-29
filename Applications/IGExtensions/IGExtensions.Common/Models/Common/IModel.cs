using System.Windows;

namespace IGExtensions.Common.Models
{
    public interface ISelectable
    {
        bool IsSelected { get; set; }
    }

    public interface IVisible
    {
        bool IsVisible { get; set; }
        Visibility Visibility { get; set; }
    }
}