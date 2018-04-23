@Code
    Dim grid = Html.DevExpress().GridView(Sub(settings)
    
        settings.Name = "CategoryGridView" & ViewData("categoryKey")
        settings.CallbackRouteValues = new with { .Controller = "Home", .Action = "CategoryGridViewPartial", .key = ViewData("categoryKey") }
        settings.Width = Unit.Percentage(100)
        settings.Columns.Add("CategoryName")
        settings.Columns.Add("Description")      
        settings.SettingsPager.Visible = true
        settings.Settings.ShowGroupPanel = false
        settings.Settings.ShowFilterRow = false
        settings.SettingsBehavior.AllowSelectByRowClick = false
        settings.SetDataRowTemplateContent(Sub(container)
            Html.RenderPartial("CategoryRow", container.DataItem)     
        End Sub)
    End Sub)
End Code
@grid.Bind(Model).GetHtml()