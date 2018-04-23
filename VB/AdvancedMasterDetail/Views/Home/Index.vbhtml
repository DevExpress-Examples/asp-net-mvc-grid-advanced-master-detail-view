@Code
    ViewBag.Title = "Advanced Master-Detail View"
End Code

<h2>Advanced Master-Detail View</h2>
@Using (Html.BeginForm())
    @Html.Action("MasterGridViewPartial")
End Using
