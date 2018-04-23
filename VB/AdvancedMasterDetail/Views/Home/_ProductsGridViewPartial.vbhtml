@Code
    Dim grid = Html.DevExpress().GridView(Sub(settings)

                                                  settings.Name = "ProductsGridView" & ViewData("productKey")
                                                  settings.CallbackRouteValues = New With {.Controller = "Home", .Action = "ProductsGridViewPartial", .key = ViewData("productKey")}
                                                  settings.Width = Unit.Percentage(100)
                                                  settings.SettingsEditing.AddNewRowRouteValues = New With {.Controller = "Home", .Action = "ProductsGridViewPartialAddNew", .key = ViewData("productKey")}
                                                  settings.SettingsEditing.UpdateRowRouteValues = New With {.Controller = "Home", .Action = "ProductsGridViewPartialUpdate", .key = ViewData("productKey")}
                                                  settings.SettingsEditing.DeleteRowRouteValues = New With {.Controller = "Home", .Action = "ProductsGridViewPartialDelete", .key = ViewData("productKey")}
                                                  settings.SettingsEditing.Mode = GridViewEditingMode.EditFormAndDisplayRow
                                                  settings.SettingsBehavior.ConfirmDelete = True

                                                  'uncomment the below lines to enable CRUD opeartions 
                                                  'settings.CommandColumn.Visible = True
                                                  'settings.CommandColumn.ShowNewButtonInHeader = True
                                                  'settings.CommandColumn.ShowDeleteButton = True
                                                  'settings.CommandColumn.ShowEditButton = True

                                                  settings.KeyFieldName = "ProductID"

                                                  settings.SettingsPager.Visible = True
                                                  settings.Settings.ShowGroupPanel = False
                                                  settings.Settings.ShowFilterRow = False
                                                  settings.SettingsBehavior.AllowSelectByRowClick = False
                                                  settings.SettingsDetail.ShowDetailRow = True
                                                  settings.SettingsDetail.ShowDetailButtons = True
                                                  settings.Settings.ShowFooter = True
      
                                                  settings.Columns.Add(Sub(col)
                                                                               col.FieldName = "ProductID"
                                                                               col.EditFormSettings.Visible = DefaultBoolean.False
                                                                       End Sub)
                                                  settings.Columns.Add("ProductName")
                                                  settings.Columns.Add(Sub(column)
                                                                               column.FieldName = "UnitPrice"
                                                                               column.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                               Dim p As SpinEditProperties = TryCast(column.PropertiesEdit, SpinEditProperties)
                                                                               p.DisplayFormatInEditMode = True
                                                                               p.DecimalPlaces = 2
                                                                               p.DisplayFormatString = "c"
                                                                       End Sub)
                                                  settings.Columns.Add(Sub(column)
                                                                               column.FieldName = "UnitsInStock"
                                                                               column.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                               Dim p As SpinEditProperties = TryCast(column.PropertiesEdit, SpinEditProperties)
                                                                               p.DisplayFormatInEditMode = True
                                                                               p.DisplayFormatString = "N2"
                                                                               p.DecimalPlaces = 2
                                                                       End Sub)
                                                  settings.Columns.Add(Sub(column)
                                                                               column.FieldName = "Discontinued"
                                                                               column.ColumnType = MVCxGridViewColumnType.CheckBox
                                                                               
                                                                       End Sub)
                                                  settings.Columns.Add(Sub(column)
                                                                               column.FieldName = "Category.CategoryID"
                                                                               column.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                               Dim cbp As ComboBoxProperties = CType(column.PropertiesEdit, ComboBoxProperties)
                                                                               cbp.DataSource = AdvancedMasterDetail.Controllers.HomeController.GetCategories()
                                                                               cbp.TextField = "CategoryName"
                                                                               cbp.ValueField = "CategoryID"
                                                                               cbp.ValueType = GetType(Integer)
                                                                       End Sub)
                                                  settings.TotalSummary.Add(New ASPxSummaryItem() With {.SummaryType = DevExpress.Data.SummaryItemType.Sum, .FieldName = "UnitsInStock"})
                                                  settings.TotalSummary.Add(New ASPxSummaryItem() With {.SummaryType = DevExpress.Data.SummaryItemType.Average, .FieldName = "UnitPrice"})
                                                  settings.SetDetailRowTemplateContent(Sub(container)
                                                                                               Dim keyValue = container.KeyValue
                                                                                               Html.RenderAction("OrdersGridViewPartial", New With {Key .key = keyValue})

                                                                                       End Sub)
                                                  settings.CellEditorInitialize = Sub(s, e)
        
                                                                                          Dim editor As ASPxEdit = CType(e.Editor, ASPxEdit)
                                                                                          editor.ValidationSettings.Display = Display.Dynamic
                                                                                  End Sub
                                          End Sub)
    If ViewData("EditError") IsNot Nothing Then
        grid.SetEditErrorText(CStr(ViewData("EditError")))
    End If
End Code
@grid.Bind(Model).GetHtml()