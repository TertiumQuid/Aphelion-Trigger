<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Portal_Registration" Title="Aphelion Trigger - Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-partial">
        <h1 class="pagetitle">Register to Play Aphelion Trigger</h1>   
        <p><asp:Label ID="RegistrationLabel" runat="server" /></p>
        <asp:Panel ID="WrapperPanel" runat="server">
        <div class="content-unit">
            <aphelion:ErrorLabel ID="SaveError" runat="server" Legend="Login Error" Text="You entered an invalid username or password." Visible="false" />
            <table class="form">
                <tr><th colspan="2">1. Choose a Faction</th></tr>
                <tr>
                    <td colspan="2">
                        <p style="font-size:110%;">
                            <strong>Hover over faction name for details.</strong> Your house's abilities are determined by your faction, so select 
                            the faction most philosophically aligned with your gaming style. 
                        </p> 
                        
                        <asp:UpdatePanel ID="FactionUpdatePanel" runat="server" UpdateMode="conditional">
                            <ContentTemplate>
                                <asp:GridView ID="Factions" runat="server"   
                                    DataSourceID="FactionListDataSource"
                                    DataKeyNames="ID"   
                                    AutoGenerateColumns="false" 
                                    ShowHeader="false"  
                                    GridLines="none" 
                                    BorderColor="#2E4d7B" 
                                    BorderWidth="1" 
                                    Font-Bold="true" 
                                    Font-Size="110%">
                                    <SelectedRowStyle BackColor="yellow" Font-Size="75%" />
                                    <RowStyle BackColor="#D3DEEF" Font-Size="75%" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Select" runat="server" CommandName="Select" CommandArgument="Select" Text='Select' Visible='<%# Convert.ToInt32( Eval( "ID" ) ) > 1 %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="Faction" runat="server" Text='<%# Eval( "Name" ) %>' style="color:rgb(36,67,113);cursor:help;" /><br />
                                            <boxover:BoxOver id="FactionBoxOver" runat="server" CssBody="boxover-body input10" CssClass="boxover input10" CssHeader="boxover-header input10" SingleClickStop="true" controltovalidate="Faction" header='<%# Eval( "Name" ) %>' Body='<%# Eval( "RegistrationText" ) + "&nbsp;" %>' />    
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                       
                        <ajax:Accordion ID="Factions2" runat="server"  Visible="false"
                            SelectedIndex="0"
                            HeaderCssClass="accordion-header" 
                            HeaderSelectedCssClass="accordion-header-selected"
                            ContentCssClass="accordion-content" 
                            FadeTransitions="false" 
                            FramesPerSecond="40" 
                            TransitionDuration="250" 
                            AutoSize="Fill" Height="500" 
                            RequireOpenedPane="false" 
                            SuppressHeaderPostbacks="true">
                           <Panes>
                            <ajax:AccordionPane ID="AccordionPane1" runat="server">
                                <Header><a href="">Allied Northern Principalities</a></Header>
                                <Content>
                                    <p>
                                        The ANP was the first superpower to organize on Terra Nova. Since its formation, the faction has been building its 
                                        empire upon the pillars of industry and war, leaving no rival in pure destructive capability. A society known 
                                        for profiteering and expansionist territorial policies, The ANP is often viewed as a blind juggernaut, violent and 
                                        strong, avaricious and impulsive. 
                                    </p>
                                    <p>
                                        ANP military are reasonably disciplined, but their true strength comes from superior firepower. Their engineers 
                                        have developed a highly successful program to harness geothermal energy through vast assemblies of drilling and power 
                                        stations, allowing the faction to cheaply run heavy industry like smelting works, foundries, and manufacturing plants. 
                                    </p>
                                    <p>
                                        Houses under the ANP are essentially part of a corporate republic, with fealty guaranteed through oppressive taxation,
                                        marshal law, and a shared profit motive. This somewhat feudal mindset means that houses approach warfare as a business, 
                                        and their leadership is known for having the most honest liars on the planet.
                                    </p>
                                    <table>
                                        <tr><th style="width:50%;">ANP Forces</th><th style="width:50%;">ANP Profile</th></tr>
                                        <tr valign="top">
                                        <td>
                                            <table>
                                                <tr><td>Unit</td><td>Composition</td></tr>
                                                <tr><td>Militia</td><td>40%</td></tr>
                                                <tr><td>Military</td><td>40%</td></tr>
                                                <tr><td>Mercenary</td><td>10%</td></tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table>
                                                <tr><td>Value</td></tr>
                                                <tr><td>Power</td><td>10</td></tr>
                                                <tr><td>Protection</td><td>10</td></tr>
                                                <tr><td>Intelligence</td><td>05</td></tr>
                                                <tr><td>Affluence</td><td>12</td></tr>
                                                <tr><td>Speed</td><td>05</td></tr>
                                            </table>                                        
                                        </td>
                                        </tr>
                                    </table>
                                    <p><asp:RadioButton ID="SelectAlliedNorthernPrincipalities" runat="server" Text="I wish to fight for the Allied Northern Principalities." Checked="true" /></p>
                                </Content>
                            </ajax:AccordionPane>
                            <ajax:AccordionPane ID="AccordionPane2" runat="server">
                                <Header><a href="">The Southern Emirate</a></Header>
                                <Content>
                                    <p>
                                        The Southern Emirate is a loose confederation of minor provinces, kingdoms, rouge states, and other quasi-independent 
                                        powers formed in pragmatic response to the ANP. With little political cohesion outside a puppet emir, the Southern Emirate's inaugural 
                                        purpose has always been to obstruct the north's intensifying power.
                                    </p>
                                    <p>
                                        Internally, the Southern Emirate is a culturally diverse and exotic sovereignty, their differences as much a strength as weakness. This 
                                        fact has allowed the faction to remain agile and adaptable to external threats, while also battling intramural strife. 
                                        Relying on speed, misdirection, and peoples wars, Southern Emirate houses pick their skirmishes carefully, and often rely on their allies to 
                                        wage coordinated forays against oblivious foes.
                                    </p>             
                                    <p>
                                        The SE also acts as a nexus for religion and the arts. While the proliferation of so many value systems ensures ongoing 
                                        resentment amongst Southern Emirate houses, it also marks them as a highly principled and contemplative people, capable of equal extremes 
                                        of mercilessness and compassion in accordance with their creeds.
                                    </p>                  
                                    <p>
                                    <table>
                                        <tr><th style="width:50%;">SE Forces</th><th style="width:50%;">SE Profile</th></tr>
                                        <tr valign="top">
                                        <td>
                                            <table>
                                                <tr><td>Unit</td><td>Composition</td></tr>
                                                <tr><td>Militia</td><td>60%</td></tr>
                                                <tr><td>Military</td><td>20%</td></tr>
                                                <tr><td>Mercenary</td><td>20%</td></tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table>
                                                <tr><td>Statistic</td><td>Value</td></tr>
                                                <tr><td>Power</td><td>05</td></tr>
                                                <tr><td>Protection</td><td>10</td></tr>
                                                <tr><td>Intelligence</td><td>10</td></tr>
                                                <tr><td>Affluence</td><td>06</td></tr>
                                                <tr><td>Speed</td><td>09</td></tr>
                                            </table>                                        
                                        </td>
                                        </tr>
                                    </table>
                                    <p><asp:RadioButton ID="SelectSouthernEmirate" runat="server" Text="I wish to fight for the Southern Emirate." Checked="true"  /></p>
                                </Content>
                            </ajax:AccordionPane>
                            <ajax:AccordionPane ID="AccordionPane3" runat="server">
                                <Header><a href="">The Spire</a></Header>
                                <Content>
                                    <p>
                                        With the most resource-rich terrain on the planet, the equatorial belt of Terra Nova was the most prized and contested 
                                        territory during the early days of colonization. After centuries of stagnation and squandered effort, however, it was clear 
                                        that for the benefit of all, a concession would have to be reached.
                                    </p>
                                    <p>
                                        A secret junta was thereby formed amongst the wealthiest and most powerful rulers of the planet's houses. Agreeing to 
                                        divide the territory among themselves, these ruling legatees promoted the accord as a peace effort, but history would reveal 
                                        ruthless foresight behind their actions.
                                    </p>
                                    <p>
                                        As massive estates and huge private institutions were built along the equator, a global aristocracy emerged. 
                                        Controlling immeasurable wealth and power, they grew disinterested in hemispheric relations, retreating into their 
                                        fortified enclaves. At the height of their arrogance, all pretence was abandoned and the AphelionTriggern elite unofficially severed  
                                        all ties with their vassal interests in the north and south.
                                    </p>
                                    <p>
                                        The rest of the planet watched helplessly as great palaces arose forged of new, deviceful technology. Sinister towers reached 
                                        increasingly skyward, breaching the upper clouds, until all across the face the world an irregular halo of soaring obelisk-cities 
                                        completely surrounded the equator. Heirs of tremendous advancement, the mysterious inhabitants of these towers became known by their 
                                        namesake: The Spire.
                                    </p>
                                    <p>
                                        The Spire are privileged scions of birthright and patrimony, and as such <strong><u>you may not register under The Spire</u></strong>.  
                                    </p> 
                                </Content>
                            </ajax:AccordionPane>
                            </Panes>
                        </ajax:Accordion>                        
                            
                    </td>
                </tr>
                <tr>
                    <th colspan="2">2. Create Your House</th>
                </tr>
                <tr>
                    <td><asp:Label ID="HouseNameLabel" runat="server" AssociatedControlID="HouseName" CssClass="label" Text="House Name:" /></td>
                    <td><asp:TextBox ID="HouseName" runat="server" CssClass="textbox input4" /></td>
                </tr>
                <tr>
                    <th colspan="2">3. Enter Account Information</th>
                </tr>
                <tr>
                    <td><asp:Label ID="UsernameLabel" runat="server" AssociatedControlID="Username" CssClass="left" Text="Username:" /></td>
                    <td><asp:TextBox ID="Username" runat="server" CssClass="textbox input4" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" CssClass="left" Text="Password:" /></td>
                    <td><asp:TextBox ID="Password" runat="server" CssClass="textbox input4" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email" CssClass="left" Text="Email:" /></td>
                    <td><asp:TextBox ID="Email" runat="server" CssClass="textbox input4" /></td>
                </tr>
                <tr>
                    <th colspan="2">4. Agree to the Player Protocols</th>
                </tr>
                <tr>
                    <td colspan="2"><div style="overflow:scroll;height:250px;background-color:rgb(82,113,131);color:rgb(255,255,255);border:1px solid rgb(255,255,255);padding:5px;" CssClass="textbox input8"><asp:Label ID="PlayerProtocols" runat="server" />&nbsp;</div></td>
                </tr>
                <tr>
                    <td colspan="2"><asp:CheckBox ID="PlayerProtocolsAgreement" runat="server" Text="I Have Read and Aggree to the Protocols" /></td>
                </tr>
                <tr>
                    <td colspan="2"><asp:LinkButton ID="SubmitRegistration" runat="server" CssClass="button" Text="Start Playing" style="width:100px;" /></td>
                </tr>
            </table>
        </div> 
        <csla:CslaDataSource ID="FactionListDataSource" runat="server" 
            TypeName="AphelionTrigger.Library.FactionList" 
            TypeAssemblyName="AphelionTrigger.Library"
            OnSelectObject="FactionListDataSource_SelectObject" />
        </asp:Panel>
    </div>        
    
    <div class="sidebar">
        <aphelion:HouseLeaderboard ID="ATHouseLeaderboard" runat="server" />
        <aphelion:GuildLeaderboard ID="ATGuildLeaderboard" runat="server" />
    </div>     
</div>
</asp:Content>

