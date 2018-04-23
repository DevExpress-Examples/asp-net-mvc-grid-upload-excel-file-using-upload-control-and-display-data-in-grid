@ModelType System.Data.DataTable
@Html.DevExpress().GridView(Sub(settings)
                                     settings.Name = "GridView1"
                                     settings.CallbackRouteValues = New With {Key .Controller = "Home", Key .Action = "GridViewPartial"}
                                     settings.SettingsPager.PageSize = 100
                                     settings.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords
                                 End Sub).Bind(Model).GetHtml()
