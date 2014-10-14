<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddDonation.ascx.cs" Inherits="Wonderland.Web.Dashboards.AddDonation" %>

<asp:Label runat="server" AssociatedControlID="partyGuidTextBox" Text="Party Guid" />
<asp:TextBox runat="server" ID="partyGuidTextBox" />
<asp:RequiredFieldValidator runat="server" ControlToValidate ="partyGuidTextBox" Text="required" />


<asp:Label runat="server" AssociatedControlID="amountTextBox" Text="Amount" />
<asp:TextBox runat="server" ID="amountTextBox" />
<asp:RequiredFieldValidator runat="server" ControlToValidate="amountTextBox" Text="required" />
<asp:CompareValidator runat="server" ControlToValidate="amountTextBox" Operator="DataTypeCheck" Type="Currency" Text="amount must be a value" />
<asp:CompareValidator runat="server" ControlToValidate="amountTextBox" Operator="GreaterThan" ValueToCompare="0" Text="amount must be greater than 0" />


<asp:Label runat="server" AssociatedControlID="giftAidCheckBox" Text="Gift Aid" />
<asp:CheckBox runat="server" ID="giftAidCheckBox" />


<hr />
<asp:Button runat="server" id="addDonationButton" UseSubmitBehavior="true" Text="Add Donation" />

<asp:literal runat="server" ID="resultLiteral" EnableViewState="false" />