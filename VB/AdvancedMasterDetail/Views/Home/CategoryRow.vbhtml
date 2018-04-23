<table class="categoryTable">
    <tr>
        <td class="imageCell">
            @Html.DevExpress().BinaryImage(Sub(settings)
                                               settings.Name = "BinaryImage" & DataBinder.Eval(Model, "CategoryID")
                                               settings.Width = 90
                                               settings.Height = 60
                                           End Sub).Bind(CType(DataBinder.Eval(Model, "Picture"), System.Data.Linq.Binary).ToArray()).GetHtml()
        </td>
        <td class="textCell">
            <div class="label">
                CATEGORY NAME
            </div>
            <div class="description">
                @DataBinder.Eval(Model, "CategoryName")
            </div>
            <br />
            <div class="label">
                DESCRIPTION
            </div>
            <div class="description">
                @DataBinder.Eval(Model, "Description")
            </div>
        </td>
    </tr>
</table>
