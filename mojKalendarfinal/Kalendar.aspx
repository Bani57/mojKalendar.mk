<%@ Page Title="" Language="C#" MasterPageFile="~/Kalendar.Master" AutoEventWireup="true" CodeBehind="Kalendar.aspx.cs" Inherits="mojKalendarfinal.Kalendar1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Kalendar.css" rel="stylesheet" type="text/css" runat="server"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label class=welcome ID="lblWelcome" runat="server" CssClass="welcome"></asp:Label>
    <asp:Calendar class=calendar align="center" ID="calEvents" runat="server" ShowGridLines="True" Height="500px" Width="50%" BorderWidth="5px" OnVisibleMonthChanged="calEvents_VisibleMonthChanged" OnSelectionChanged="calEvents_SelectionChanged" BorderColor="#757575" BorderStyle="Groove">
        <DayHeaderStyle BorderWidth="3px" />
        <DayStyle BorderWidth="3px" />
        <OtherMonthDayStyle ForeColor="Gray" />
    </asp:Calendar>
    <asp:Image class=image ID="imgSide" runat="server" ImageUrl="~/Images/6.jpg"/>
    <div id="buttons">
    <asp:Button CssClass="button green" ID="btnSignOut" runat="server" OnClick="btnSignOut_Click" Text="Sign out" CausesValidation="False" />
    <asp:Button CssClass="button red" ID="btnCustomize" runat="server" OnClick="btnCustomize_Click" Text="Customize calendar" CausesValidation="False" />
    <asp:Button CssClass="button yellow" ID="btnAddEvent" runat="server" Enabled="False" OnClick="btnAddEvent_Click" Text="Add event" CausesValidation="False" />
    <asp:Button CssClass="button blue" ID="btnViewEvents" runat="server" OnClick="btnViewEvents_Click" Text="View events" CausesValidation="False" />
    </div>
        <asp:RadioButtonList align="center" CssClass="option radioblue" ID="rblViewEvents" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblViewEvents_SelectedIndexChanged" RepeatDirection="Horizontal" Visible="False" Width="50%">
            <asp:ListItem Selected="True">For today</asp:ListItem>
            <asp:ListItem>For this week</asp:ListItem>
            <asp:ListItem>Next 5 events</asp:ListItem>
            <asp:ListItem>All events</asp:ListItem>
        </asp:RadioButtonList>
    <asp:Panel ID="pnlNoEvents" runat="server" BorderColor="#757575" BorderStyle="Groove" BorderWidth="3px" CssClass="noevents" Visible="False">
        <asp:Label ID="lblEmpty" runat="server"></asp:Label>
    </asp:Panel>
        <asp:GridView CssClass="events" align="center" ID="gvEvents" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#757575" BorderStyle="Groove" BorderWidth="5px" CellPadding="3" OnRowDeleting="gvEvents_RowDeleting" Visible="False" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvEvents_PageIndexChanging" OnSorting="gvEvents_Sorting" PageSize="5">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="EventName" HeaderText="Name" SortExpression="EventName" ReadOnly="True" >
                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditDescription" runat="server" CssClass="text" MaxLength="64" placeholder="Event description" Rows="4" Text='<%# Bind("Description") %>' TextMode="MultiLine"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>
                <asp:BoundField DataField="Date" DataFormatString="{0:dd.MM.yyyy}" HeaderText="Date" SortExpression="Date" ReadOnly="True" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Time" HeaderText="Time" SortExpression="Time" DataFormatString="{0:HH:mm}" ReadOnly="True" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:CommandField ButtonType="Button" ShowDeleteButton="True" DeleteText="Delete event">
                <ControlStyle CssClass="button2 blue" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="False" ForeColor="Black" Font-Italic="True" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
        </asp:GridView>
    <asp:Panel CssClass="customize" ID="pnlCustomize" runat="server" Visible="False" BorderColor="#757575" BorderStyle="Groove" Width="50%" BorderWidth="3px">
        <table align="center">
            <tr>
                <td>Calendar size</td>
                <td>
                    <asp:RadioButtonList CssClass="radiored" ID="rblSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblSize_SelectedIndexChanged" RepeatDirection="Horizontal" >
                        <asp:ListItem Value="35">Small</asp:ListItem>
                        <asp:ListItem Selected="True" Value="50">Medium</asp:ListItem>
                        <asp:ListItem Value="75">Large</asp:ListItem>
                        <asp:ListItem Value="100">Full</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>Background color</td>
                <td>
                    <asp:DropDownList CssClass="dropdown red" ID="ddlBackground" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBackground_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Header color</td>
                <td>
                    <asp:DropDownList CssClass="dropdown red" ID="ddlHeader" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlHeader_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Calendar text size</td>
                <td>
                    <asp:RadioButtonList ID="rblCalendarTextSize" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rblCalendarTextSize_SelectedIndexChanged" CssClass="radiored">
                        <asp:ListItem Selected="True" Value="Large">Small</asp:ListItem>
                        <asp:ListItem Value="Larger">Medium</asp:ListItem>
                        <asp:ListItem Value="XLarge">Large</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>Day border style</td>
                <td>
                    <asp:DropDownList CssClass="dropdown red" ID="ddlDayBorderStyle" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDayBorderStyle_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Weekday name style</td>
                <td>
                    <asp:RadioButtonList ID="rblWeekdayName" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rblWeekdayName_SelectedIndexChanged" CssClass="radiored">
                        <asp:ListItem Selected="True" Value="Short">Short</asp:ListItem>
                        <asp:ListItem Value="Full">Full</asp:ListItem>
                        <asp:ListItem Value="FirstLetter">First letter</asp:ListItem>
                        <asp:ListItem Value="FirstTwoLetters">First two letters</asp:ListItem>
                        <asp:ListItem Value="Shortest">As short as possible</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>Weekday name size</td>
                <td>
                    <asp:RadioButtonList ID="rblWeekdayTextSize" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rblWeekdayTextSize_SelectedIndexChanged" CssClass="radiored">
                        <asp:ListItem Selected="True" Value="Large">Small</asp:ListItem>
                        <asp:ListItem Value="Larger">Medium</asp:ListItem>
                        <asp:ListItem Value="XLarge">Large</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>Today color</td>
                <td>
                    <asp:DropDownList CssClass="dropdown red" ID="ddlTodayColor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTodayColor_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Selected day color</td>
                <td>
                    <asp:DropDownList CssClass="dropdown red" ID="ddlSelectedDayColor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSelectedDayColor_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Show previous and next month names</td>
                <td>
                    <asp:CheckBox ID="chbShowMonths" runat="server" AutoPostBack="True" OnCheckedChanged="chbShowMonths_CheckedChanged" CssClass="radiored" Text="Show month names" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnPreferences" runat="server" OnClick="btnPreferences_Click" Text="Save preferences" CssClass="button2 red" />
                </td>
                <td>
                    <asp:Button ID="btnSetDefaults" runat="server" OnClick="btnSetDefaults_Click" Text="Set defaults" CssClass="button2 red" />
                </td>
            </tr>
        </table>
</asp:Panel>
        <asp:Panel ID="pnlEvent" runat="server" Visible="False" BorderColor="#757575" BorderStyle="Groove" BorderWidth="3px" CssClass="event" Width="50%">
            <table align="center">
                <tr>
                    <td>Event for: </td>
                    <td>
                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>Enter name of event:</td>
                    <td>
                        <asp:TextBox placeholder="Name of event" CssClass="textbox" ID="txtEventName" runat="server" MaxLength="30"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEventName" Display="Dynamic" ErrorMessage="You must enter a name for the event!" Font-Bold="True" ForeColor="#F2CF66"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Describe the event:</td>
                    <td>
                        <asp:TextBox placeholder="Event description" CssClass="text" ID="txtDescription" runat="server" MaxLength="64" Rows="4" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescription" Display="Dynamic" ErrorMessage="You must describe the event!" Font-Bold="True" ForeColor="#F2CF66"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>When will it start?</td>
                    <td>
                        <asp:DropDownList CssClass="dropdown yellow" ID="ddlStartHours" runat="server">
                        </asp:DropDownList><span id="time">:</span><asp:DropDownList CssClass="dropdown yellow" ID="ddlStartMinutes" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSaveEvent" runat="server" OnClick="btnSaveEvent_Click" Text="Schedule event" CssClass="button2 yellow" />
                    </td>
                    <td>
                        <asp:Button ID="btnClearEvent" runat="server" OnClick="btnClearEvent_Click" Text="Clear info" CssClass="button2 yellow" CausesValidation="False" />
                    </td>
                    <td></td>
                </tr>
            </table>
        </asp:Panel>
</asp:Content>
