<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RefundDonation.ascx.cs" Inherits="Wonderland.Web.Dashboards.RefundDonation" %>

<h3>Refund Donation</h3>

<asp:Label runat="server" AssociatedControlID="vendorTxCodeTextBox" Text="VendorTxCode" />
<asp:TextBox runat="server" ID="vendorTxCodeTextBox" />
<asp:RequiredFieldValidator runat="server" ControlToValidate="vendorTxCodeTextBox" Text="required" />


<hr />
<asp:Button runat="server" id="refundDonationButton" UseSubmitBehavior="true" Text="Refund Donation" />

<asp:literal runat="server" ID="resultLiteral" EnableViewState="false" />