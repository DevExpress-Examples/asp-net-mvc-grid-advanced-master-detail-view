@Code
    Dim grid = Html.DevExpress().GridView(Sub(settings)
                                                  settings.Name = "MasterGridView"
                                                  settings.CallbackRouteValues = New With {.Controller = "Home", .Action = "MasterGridViewPartial"}
                                                  settings.Width = 800
                                                  settings.SettingsEditing.AddNewRowRouteValues = New With {.Controller = "Home", .Action = "MasterGridViewPartialAddNew"}
                                                  settings.SettingsEditing.UpdateRowRouteValues = New With {.Controller = "Home", .Action = "MasterGridViewPartialUpdate"}
                                                  settings.SettingsEditing.DeleteRowRouteValues = New With {.Controller = "Home", .Action = "MasterGridViewPartialDelete"}
                                                  settings.SettingsEditing.Mode = GridViewEditingMode.EditFormAndDisplayRow
                                                  settings.SettingsBehavior.ConfirmDelete = True
                                                  
                                                  'uncomment the below lines to enable CRUD opeartions 
                                                  'settings.CommandColumn.Visible = True
                                                  'settings.CommandColumn.ShowNewButtonInHeader = True
                                                  'settings.CommandColumn.ShowDeleteButton = True
                                                  'settings.CommandColumn.ShowEditButton = True

                                                  settings.KeyFieldName = "SupplierID"
       
                                                  settings.SettingsDetail.ShowDetailButtons = True
                                                  settings.SettingsDetail.ShowDetailRow = True
                                                  settings.SettingsPager.Visible = True
                                                  settings.Settings.ShowGroupPanel = False
                                                  settings.Settings.ShowFilterRow = False
                                                  settings.SettingsBehavior.AllowSelectByRowClick = False

                                                  settings.Columns.Add(Sub(col)
                                                                               col.FieldName = "SupplierID"
                                                                               col.Width = 70
                                                                               col.EditFormSettings.Visible = DefaultBoolean.False
                                                                       End Sub)
                                                  settings.Columns.Add("CompanyName")
                                                  settings.Columns.Add("ContactName")
                                                  settings.Columns.Add("City")
                                                  settings.Columns.Add("Country")
                                                  settings.SetDetailRowTemplateContent(Sub(container)
                                                                                               Dim keyValue = container.KeyValue
                                                                                               ViewContext.Writer.Write("<div style='padding: 3px 3px 2px 3px'>")
                                                                                               Html.RenderAction("PageControlPartial", New With {.key = keyValue})
                                                                                               ViewContext.Writer.Write("</div>")
                                                                                       End Sub)
                                                  settings.CellEditorInitialize = Sub(s, e)
                                                                                          Dim editor As ASPxEdit = e.Editor
                                                                                          editor.ValidationSettings.Display = Display.Dynamic
                                                                                  End Sub
                                          End Sub)
    If ViewData("EditError") IsNot Nothing Then
        grid.SetEditErrorText(CStr(ViewData("EditError")))
    End If
End Code
@grid.Bind(Model).GetHtml()