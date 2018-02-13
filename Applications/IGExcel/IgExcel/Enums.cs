
namespace IgExcel
{
    public enum IgExcelDocumentStatus
    {
        NoDocumentLoaded,
        ExistingNotModified,
        ExistingModified,
        BlankNotModified,
        BlankModified,
        TemplateNotModified,
    }

    public enum DimensionDialogMode
    {
        RowHeight,
        ColumnWidth,
        DeafaultWidth
    }

    public enum PasswordDialogMode
    {
        OpenPassword,
        ModifyPassword
    }

    public enum WriteProtectedFileMode
    {
        ReadOnly,
        ReadAndWrite
    }
}
