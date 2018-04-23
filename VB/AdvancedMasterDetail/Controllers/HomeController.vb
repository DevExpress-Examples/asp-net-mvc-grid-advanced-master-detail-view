Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports DevExpress.Web.Mvc
Imports AdvancedMasterDetail.Models

Namespace AdvancedMasterDetail.Controllers
	Public Class HomeController
		Inherits Controller
		'
		' GET: /Home/

		Private context As New NorthwindClassesDataContext()
		Public Function Index() As ActionResult

			Return View()
		End Function

		<ValidateInput(False)> _
		Public Function MasterGridViewPartial() As ActionResult
			Dim model = context.Suppliers
			Return PartialView("_MasterGridViewPartial", model)
		End Function
		Public Function PageControlPartial(ByVal key As Integer) As ActionResult
			ViewData("key") = key
			Dim model = context.Suppliers.Where(Function(item) item.SupplierID = key).FirstOrDefault()
			Return PartialView("DetailPageControl", model)
		End Function
		<HttpPost, ValidateInput(False)> _
		Public Function MasterGridViewPartialAddNew(ByVal item As AdvancedMasterDetail.Models.Supplier) As ActionResult
			Dim model = context.Suppliers
			If ModelState.IsValid Then
				Try
					' Insert here a code to insert the new item in your model
					context.Suppliers.InsertOnSubmit(item)
					context.SubmitChanges()
				Catch e As Exception
					ViewData("EditError") = e.Message
				End Try
			Else
				ViewData("EditError") = "Please, correct all errors."
			End If
			Return PartialView("_MasterGridViewPartial", model)
		End Function
		<HttpPost, ValidateInput(False)> _
		Public Function MasterGridViewPartialUpdate(ByVal item As AdvancedMasterDetail.Models.Supplier) As ActionResult
			Dim model = context.Suppliers
			If ModelState.IsValid Then
				Try
					Dim existingItem = context.Suppliers.Where(Function(x) x.SupplierID.Equals(item.SupplierID)).FirstOrDefault()
					existingItem.CompanyName = item.CompanyName
					existingItem.ContactName = item.ContactName
					existingItem.Country = item.Country
					existingItem.City = item.City
					context.SubmitChanges()
				Catch e As Exception
					ViewData("EditError") = e.Message
				End Try
			Else
				ViewData("EditError") = "Please, correct all errors."
			End If
			Return PartialView("_MasterGridViewPartial", model)
		End Function
		<HttpPost, ValidateInput(False)> _
		Public Function MasterGridViewPartialDelete(ByVal SupplierID As System.Int32) As ActionResult
			Dim model = context.Suppliers
			If SupplierID >= 0 Then
				Try
					' Insert here a code to delete the item from your model
					Dim existingItem = context.Suppliers.Where(Function(x) x.SupplierID.Equals(SupplierID)).FirstOrDefault()
					context.Suppliers.DeleteOnSubmit(existingItem)
					context.SubmitChanges()
				Catch e As Exception
					ViewData("EditError") = e.Message
				End Try
			End If
			Return PartialView("_MasterGridViewPartial", model)
		End Function



		<ValidateInput(False)> _
		Public Function ProductsGridViewPartial(ByVal key As Integer) As ActionResult
			Dim model = context.Products.Where(Function(item) item.SupplierID = key)
			ViewData("productKey") = key
			Return PartialView("_ProductsGridViewPartial", model)
		End Function

		<HttpPost, ValidateInput(False)> _
		Public Function ProductsGridViewPartialAddNew(ByVal item As AdvancedMasterDetail.Models.Product, ByVal key As Integer) As ActionResult
			Dim model = context.Products
			ViewData("productKey") = key
			If ModelState.IsValid Then
				Try
					item.SupplierID = key
					item.Supplier = context.Suppliers.Where(Function(x) x.SupplierID = key).FirstOrDefault()
					item.Category = context.Categories.Where(Function(category) category.CategoryID.Equals(item.CategoryID)).FirstOrDefault()
					model.InsertOnSubmit(item)
					context.SubmitChanges()
				Catch e As Exception
					ViewData("EditError") = e.Message
				End Try
			Else
				ViewData("EditError") = "Please, correct all errors."
			End If
			Dim querymodel = context.Products.Where(Function(it) it.SupplierID = key)
			Return PartialView("_ProductsGridViewPartial", querymodel)
		End Function
		<HttpPost, ValidateInput(False)> _
		Public Function ProductsGridViewPartialUpdate(ByVal item As AdvancedMasterDetail.Models.Product, ByVal key As Integer) As ActionResult

			Dim model = context.Products
			ViewData("productKey") = key
			If ModelState.IsValid Then
				Try
					Dim modelItem = model.FirstOrDefault(Function(it) it.ProductID = item.ProductID)
					If modelItem IsNot Nothing Then
						Me.UpdateModel(modelItem)
						context.SubmitChanges()
					End If
				Catch e As Exception
					ViewData("EditError") = e.Message
				End Try
			Else
				ViewData("EditError") = "Please, correct all errors."
			End If
			Dim querymodel = context.Products.Where(Function(it) it.SupplierID = key)
			Return PartialView("_ProductsGridViewPartial", querymodel)
		End Function
		<HttpPost, ValidateInput(False)> _
		Public Function ProductsGridViewPartialDelete(ByVal ProductID As System.Int32, ByVal key As Integer) As ActionResult

			Dim model = context.Products
			ViewData("productKey") = key
			If ProductID >= 0 Then
				Try
					Dim item = model.FirstOrDefault(Function(it) it.ProductID = ProductID)
					If item IsNot Nothing Then
						model.DeleteOnSubmit(item)
					End If
					context.SubmitChanges()
				Catch e As Exception
					ViewData("EditError") = e.Message
				End Try
			End If
			Dim querymodel = context.Products.Where(Function(item) item.SupplierID = key)
			Return PartialView("_ProductsGridViewPartial", querymodel)
		End Function


		<ValidateInput(False)> _
		Public Function CategoryGridViewPartial(ByVal key As Integer) As ActionResult
			ViewData("categoryKey") = key
			Dim model = _
				From a In context.Categories _
				Where ( _
					From b In context.Products _
					Where b.CategoryID.Equals(a.CategoryID) AndAlso b.SupplierID = key _
					Select 1).Any() _
				Select a

			Return PartialView("_CategoryGridViewPartial", model)
		End Function


		<ValidateInput(False)> _
		Public Function OrdersGridViewPartial(ByVal key As Integer) As ActionResult
			ViewData("orderKey") = key
			Dim model = context.Order_Details.Where(Function(item) item.ProductID = key)
			Return PartialView("_OrdersGridViewPartial", model)
		End Function

		<HttpPost, ValidateInput(False)> _
		Public Function OrdersGridViewPartialAddNew(ByVal item As AdvancedMasterDetail.Models.Order_Detail, ByVal key As Integer) As ActionResult
			Dim model = context.Order_Details
			ViewData("orderKey") = key
			If ModelState.IsValid Then
				Try
					item.ProductID = key
					item.Product = context.Products.Where(Function(productItem) productItem.ProductID = key).FirstOrDefault()
					model.InsertOnSubmit(item)
					context.SubmitChanges()
				Catch e As Exception
					ViewData("EditError") = e.Message
				End Try
			Else
				ViewData("EditError") = "Please, correct all errors."
			End If
			Dim querymodel = context.Order_Details.Where(Function(order) order.ProductID = key)

			Return PartialView("_OrdersGridViewPartial", querymodel)
		End Function
		<HttpPost, ValidateInput(False)> _
		Public Function OrdersGridViewPartialUpdate(ByVal item As AdvancedMasterDetail.Models.Order_Detail, ByVal key As Integer) As ActionResult
			Dim model = context.Order_Details
			ViewData("orderKey") = key
			If ModelState.IsValid Then
				Try
					Dim modelItem = model.FirstOrDefault(Function(it) it.OrderID = item.OrderID)
					If modelItem IsNot Nothing Then
						Me.UpdateModel(modelItem)
						context.SubmitChanges()
					End If
				Catch e As Exception
					ViewData("EditError") = e.Message
				End Try
			Else
				ViewData("EditError") = "Please, correct all errors."
			End If

			Dim querymodel = context.Order_Details.Where(Function(order) order.ProductID = key)
			Return PartialView("_OrdersGridViewPartial", querymodel)
		End Function
		<HttpPost, ValidateInput(False)> _
		Public Function OrdersGridViewPartialDelete(ByVal OrderID As System.Int32, ByVal key As Integer) As ActionResult
			Dim model = context.Order_Details
			ViewData("orderKey") = key
			If OrderID >= 0 Then
				Try
					Dim item = model.FirstOrDefault(Function(it) it.OrderID = OrderID)
					If item IsNot Nothing Then
						model.DeleteOnSubmit(item)
					End If
					context.SubmitChanges()
				Catch e As Exception
					ViewData("EditError") = e.Message
				End Try
			End If
			Dim querymodel = context.Order_Details.Where(Function(order) order.ProductID = key)
			Return PartialView("_OrdersGridViewPartial", querymodel)
		End Function
		Public Shared Function GetCategories() As IQueryable
			Return New NorthwindClassesDataContext().Categories
		End Function
	End Class

End Namespace
