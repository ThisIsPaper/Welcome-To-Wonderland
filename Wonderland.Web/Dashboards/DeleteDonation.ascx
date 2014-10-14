<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeleteDonation.ascx.cs" Inherits="Wonderland.Web.Dashboards.DeleteDonation" %>

<h3>Delete Donation</h3>

<asp:Label runat="server" AssociatedControlID="vendorTxCodeTextBox" Text="VendorTxCode" />
<asp:TextBox runat="server" ID="vendorTxCodeTextBox" />
<asp:RequiredFieldValidator runat="server" ControlToValidate="vendorTxCodeTextBox" Text="required" />


<hr />
<asp:Button runat="server" id="deleteDonationButton" UseSubmitBehavior="true" Text="Delete Donation" />

<asp:literal runat="server" ID="resultLiteral" EnableViewState="false" />