@Code
    Dim grid = Html.DevExpress().GridView(Sub(settings)

                                                  settings.Name = "OrdersGridView" & ViewData("orderKey")
                                                  settings.CallbackRouteValues = New With {.Controller = "Home", .Action = "OrdersGridViewPartial", .key = ViewData("orderKey")}

                                                  settings.SettingsEditing.AddNewRowRouteValues = New With {.Controller = "Home", .Action = "OrdersGridViewPartialAddNew", .key = ViewData("orderKey")}
                                                  settings.SettingsEditing.UpdateRowRouteValues = New With {.Controller = "Home", .Action = "OrdersGridViewPartialUpdate", .key = ViewData("orderKey")}
                                                  settings.SettingsEditing.DeleteRowRouteValues = New With {.Controller = "Home", .Action = "OrdersGridViewPartialDelete", .key = ViewData("orderKey")}
                                                  settings.SettingsEditing.Mode = GridViewEditingMode.EditFormAndDisplayRow
                                                  settings.SettingsBehavior.ConfirmDelete = True
                                                  
                                                  'uncomment the below lines to enable CRUD opeartions 
                                                  
                                                  'settings.CommandColumn.Visible = True
                                                  'settings.CommandColumn.ShowNewButtonInHeader = True
                                                  'settings.CommandColumn.ShowDeleteButton = True
                                                  'settings.CommandColumn.ShowEditButton = True
                                                  
                                                  settings.CommandColumn.ShowClearFilterButton = True
                                                  settings.KeyFieldName = "OrderID"

                                                  settings.SettingsPager.Visible = True
                                                  settings.Settings.ShowGroupPanel = True
                                                  settings.Settings.ShowFilterRow = True
                                                  settings.SettingsBehavior.AllowSelectByRowClick = False
                                                  settings.Settings.ShowFooter = True
        
                                                  settings.Columns.Add("OrderID").EditFormSettings.Visible = DefaultBoolean.False
                                                  settings.Columns.Add(Sub(column)
        
                                                                               column.FieldName = "UnitPrice"
                                                                               column.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                               Dim p As SpinEditProperties = TryCast(column.PropertiesEdit, SpinEditProperties)
                                                                               p.DisplayFormatInEditMode = True
                                                                               p.DecimalPlaces = 2
                                                                               p.DisplayFormatString = "c"
                                                                       End Sub)
                                                  settings.Columns.Add(Sub(column)
                                                                               column.FieldName = "Quantity"
                                                                              
                                                                               column.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                               Dim p As SpinEditProperties = TryCast(column.PropertiesEdit, SpinEditProperties)
                                                                               p.DecimalPlaces = 0
                                                                               p.NumberType = SpinEditNumberType.Integer
                                                                       End Sub)
                                                  settings.Columns.Add(Sub(column)
                                                                               column.FieldName = "Discount"
                                                                               column.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                               Dim p As SpinEditProperties = TryCast(column.PropertiesEdit, SpinEditProperties)
                                                                               p.DisplayFormatInEditMode = True
                                                                               p.DisplayFormatString = "N2"
                                                                               p.DecimalPlaces = 2
                                                                       End Sub)
                                                  settings.Columns.Add(Sub(column)

                                                                               column.FieldName = "Total"
                                                                               column.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                               Dim p As SpinEditProperties = TryCast(column.PropertiesEdit, SpinEditProperties)
                                                                               p.DisplayFormatInEditMode = True
                                                                               p.DecimalPlaces = 2
                                                                               column.EditFormSettings.Visible = DefaultBoolean.False
                                                                               column.UnboundType = DevExpress.Data.UnboundColumnType.Decimal
                                                                               column.PropertiesEdit.DisplayFormatString = "c"
                                                                               column.UnboundExpression = "[Quantity] * [UnitPrice] * (1 - [Discount])"
                                                                       End Sub)
                                                  settings.TotalSummary.Add(New ASPxSummaryItem() With {.SummaryType = DevExpress.Data.SummaryItemType.Sum, .FieldName = "Total"})
        
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