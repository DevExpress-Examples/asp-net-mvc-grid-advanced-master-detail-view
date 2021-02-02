    @Html.DevExpress().PageControl(Sub(settings)

                                        settings.Name = "PageControl" & ViewData("key")
                                        settings.Width = Unit.Percentage(100)
                                        settings.CallbackRouteValues = New With {.Controller = "Home", .Action = "PageControlPartial", .key = ViewData("key")}
                                        settings.TabPages.Add("Products").SetContent(Sub()

                                                                                         Html.RenderAction("ProductsGridViewPartial", New With {.key = ViewData("key")})

                                                                                     End Sub)
                                        settings.TabPages.Add("Categories").SetContent(Sub()

                                                                                           Html.RenderAction("CategoryGridViewPartial", New With {.key = ViewData("key")})

                                                                                       End Sub)
                                        settings.TabPages.Add("Address").SetContent(Sub()

                                                                                        Html.RenderPartial("DetailInformation")
                                                                                    End Sub)
                                    End Sub).GetHtml()

