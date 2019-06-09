using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Windows.Forms;
using System.Data;
using System.Reflection;

namespace InvoicesGenerator
{
    public static class UserControl
    {
        public enum dummyType
        {
            dummy
        }

        public static Dictionary<int, String> _userLevels = new System.Collections.Generic.Dictionary<int, string>();

        public static void GetAllClasses(string nameSpace)  //List<string> 
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            List<string> namespaceList = new List<string>();
            List<string> returnList = new List<string>();
            foreach (Type type in asm.GetTypes())
            {
                if (type.Namespace == nameSpace && type.BaseType.Name == "Form")
                {
                    namespaceList.Add(type.Name);
                    using (Form frm = (Form)Activator.CreateInstance(type, new object[] { UserControl.dummyType.dummy }))
                    {
                        using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                        {
                            cn.Open();
                            UserControl.ProcessControls(cn, frm, frm.GetType().Name);
                            
                            
                            cn.Close();
                        }
                    }
                }
            }
        }

        public static void initLevelDictionary()
        {
            using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
            {
                cn.Open();
                using (SqlCeCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        cmd.CommandText = @"Select hierarchy, AuthLevel_name from auth_levels";

                        cmd.Parameters.Clear();
                        SqlCeDataReader levelReader = cmd.ExecuteReader();

                        while (levelReader.Read())
                        {
                            if (!_userLevels.ContainsKey(levelReader.GetInt32(levelReader.GetOrdinal("hierarchy"))))
                            {
                                _userLevels.Add(levelReader.GetInt32(levelReader.GetOrdinal("hierarchy")), levelReader.GetString(levelReader.GetOrdinal("AuthLevel_name")));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        General.ErrorMessage(e.Message);
                    }
                }
            }
        }

        public static Dictionary<string, int> initFormControlsDictionary(string FormName)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
            {
                cn.Open();
                using (SqlCeCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        cmd.CommandText = @"select control,Hierarchy 
                                            from control_levels
                                            inner join auth_levels on auth_levels.AuthLevel_id =control_levels.control_level
                                            where form =@form";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@form", FormName);
                        SqlCeDataReader controlLevelReader = cmd.ExecuteReader();

                        while (controlLevelReader.Read())
                        {
                            result.Add(controlLevelReader.GetString(controlLevelReader.GetOrdinal("control")), controlLevelReader.GetInt32(controlLevelReader.GetOrdinal("hierarchy")));
                        }
                    }
                    catch (Exception e)
                    {
                        General.ErrorMessage(e.Message);
                    }
                }
            }
            return result;
        }

        private static void ProcessControls(SqlCeConnection cn, Control ctrlContainer, String FormName)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                using (SqlCeCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        cmd.CommandText = @"
                                INSERT INTO [control_levels]
                                       ([form],[control]
                                       ,[control_level])
                                 select @form,@control,(select authLevel_id from auth_levels where AuthLevel_name =@levelName)
                                 where not exists(select 1 from control_levels where form = @form and control = @control)";

                        if (!String.IsNullOrEmpty(FormName) && !String.IsNullOrEmpty(ctrl.Name))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@form", FormName);
                            cmd.Parameters.AddWithValue("@control", ctrl.Name);
                            int controlHierarchy = 0;
                            if (ctrl.Tag != null)
                            {
                                Int32.TryParse(ctrl.Tag.ToString(), out controlHierarchy);
                            }
                            cmd.Parameters.AddWithValue("@levelName", _userLevels[controlHierarchy > 0 ? controlHierarchy : 100]);

                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = @"
                                update [control_levels]
                                    set [control_level]=(select authLevel_id from auth_levels where AuthLevel_name =@levelName)
                                 where form = @form and control = @control and [control_level]<>(select authLevel_id from auth_levels where AuthLevel_name =@levelName)";

                        if (!String.IsNullOrEmpty(FormName) && !String.IsNullOrEmpty(ctrl.Name))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@form", FormName);
                            cmd.Parameters.AddWithValue("@control", ctrl.Name);
                            int controlHierarchy = 100;
                            if (ctrl.Tag != null)
                            {
                                Int32.TryParse(ctrl.Tag.ToString(), out controlHierarchy);
                            }
                            cmd.Parameters.AddWithValue("@levelName", _userLevels[controlHierarchy > 0 ? controlHierarchy : 100]);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
                if (ctrl.HasChildren)
                {
                    ProcessControls(cn, ctrl, FormName);
                }

                if (ctrl.GetType().Name.Equals("MenuStrip"))
                {
                    ProcessMenuControls(cn, (MenuStrip)ctrl, FormName);
                }

                if (ctrl.GetType().Name.Equals("ToolStrip"))
                {
                    ProcessToolStripControls(cn, (ToolStrip)ctrl, FormName);
                }

                if (ctrl.GetType().Name.Equals("TabControl"))
                {
                    ProcessTabControlControls(cn, (TabControl)ctrl, FormName);
                }
            }
        }

        private static void ProcessToolStripControls(SqlCeConnection cn, ToolStrip toolContainer, String FormName)
        {
            foreach (ToolStripItem item in toolContainer.Items)
            {
                using (SqlCeCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        cmd.CommandText = @"
                                INSERT INTO [control_levels]
                                       ([form],[control]
                                       ,[control_level])
                                 select @form,@control,(select authLevel_id from auth_levels where AuthLevel_name =@levelName)
                                 where not exists(select 1 from control_levels where form = @form and control = @control)";

                        if (!String.IsNullOrEmpty(FormName) && !String.IsNullOrEmpty(item.Name))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@form", FormName);
                            cmd.Parameters.AddWithValue("@control", item.Name);
                            int controlHierarchy = 100;
                            if (item.Tag != null)
                            {
                                Int32.TryParse(item.Tag.ToString(), out controlHierarchy);
                            }
                            cmd.Parameters.AddWithValue("@levelName", _userLevels[controlHierarchy > 0 ? controlHierarchy : 100]);

                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = @"
                                update [control_levels]
                                    set [control_level]=(select authLevel_id from auth_levels where AuthLevel_name =@levelName)
                                 where form = @form and control = @control and [control_level]<>(select authLevel_id from auth_levels where AuthLevel_name =@levelName)";

                        if (!String.IsNullOrEmpty(FormName) && !String.IsNullOrEmpty(item.Name))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@form", FormName);
                            cmd.Parameters.AddWithValue("@control", item.Name);
                            int controlHierarchy = 100;
                            if (item.Tag != null)
                            {
                                Int32.TryParse(item.Tag.ToString(), out controlHierarchy);
                            }
                            cmd.Parameters.AddWithValue("@levelName", _userLevels[controlHierarchy > 0 ? controlHierarchy : 100]);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
               

                if (item.GetType().Name.Equals("ToolStripDropDownButton"))
                {
                    ProcessButtonToolstripItems(cn, (ToolStripDropDownButton)item, FormName);
                }

            }
        }

        private static void ProcessMenuControls(SqlCeConnection cn, MenuStrip menuContainer, String FormName)
        {
            foreach (ToolStripMenuItem item in menuContainer.Items)
            {
                using (SqlCeCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        cmd.CommandText = @"
                                INSERT INTO [control_levels]
                                       ([form],[control]
                                       ,[control_level])
                                 select @form,@control,(select authLevel_id from auth_levels where AuthLevel_name =@levelName)
                                 where not exists(select 1 from control_levels where form = @form and control = @control)";

                        if (!String.IsNullOrEmpty(FormName) && !String.IsNullOrEmpty(item.Name))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@form", FormName);
                            cmd.Parameters.AddWithValue("@control", item.Name);
                            int controlHierarchy = 100;
                            if (item.Tag != null)
                            {
                                Int32.TryParse(item.Tag.ToString(), out controlHierarchy);
                            }
                            cmd.Parameters.AddWithValue("@levelName", _userLevels[controlHierarchy > 0 ? controlHierarchy : 100]);

                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = @"
                                update [control_levels]
                                    set [control_level]=(select authLevel_id from auth_levels where AuthLevel_name =@levelName)
                                 where form = @form and control = @control and [control_level]<>(select authLevel_id from auth_levels where AuthLevel_name =@levelName)";

                        if (!String.IsNullOrEmpty(FormName) && !String.IsNullOrEmpty(item.Name))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@form", FormName);
                            cmd.Parameters.AddWithValue("@control", item.Name);
                            int controlHierarchy = 100;
                            if (item.Tag != null)
                            {
                                Int32.TryParse(item.Tag.ToString(), out controlHierarchy);
                            }
                            cmd.Parameters.AddWithValue("@levelName", _userLevels[controlHierarchy > 0 ? controlHierarchy : 100]);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
                if (item.HasDropDownItems)
                {
                    ProcessSubMenuItems(cn, item, FormName);
                }
            }
        }


        private static void ProcessTabControlControls(SqlCeConnection cn, TabControl tabControl, String FormName)
        {
            foreach (TabPage page in tabControl.TabPages)
            {
                using (SqlCeCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        cmd.CommandText = @"
                                INSERT INTO [control_levels]
                                       ([form],[control]
                                       ,[control_level])
                                 select @form,@control,(select authLevel_id from auth_levels where AuthLevel_name =@levelName)
                                 where not exists(select 1 from control_levels where form = @form and control = @control)";

                        if (!String.IsNullOrEmpty(FormName) && !String.IsNullOrEmpty(page.Name))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@form", FormName);
                            cmd.Parameters.AddWithValue("@control", page.Name);
                            int controlHierarchy = 100;
                            if (page.Tag != null)
                            {
                                Int32.TryParse(page.Tag.ToString(), out controlHierarchy);
                            }
                            cmd.Parameters.AddWithValue("@levelName", _userLevels[controlHierarchy > 0 ? controlHierarchy : 100]);

                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = @"
                                update [control_levels]
                                    set [control_level]=(select authLevel_id from auth_levels where AuthLevel_name =@levelName)
                                 where form = @form and control = @control and [control_level]<>(select authLevel_id from auth_levels where AuthLevel_name =@levelName)";

                        if (!String.IsNullOrEmpty(FormName) && !String.IsNullOrEmpty(page.Name))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@form", FormName);
                            cmd.Parameters.AddWithValue("@control", page.Name);
                            int controlHierarchy = 100;
                            if (page.Tag != null)
                            {
                                Int32.TryParse(page.Tag.ToString(), out controlHierarchy);
                            }
                            cmd.Parameters.AddWithValue("@levelName", _userLevels[controlHierarchy > 0 ? controlHierarchy : 100]);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
        }

        private static void ProcessButtonToolstripItems(SqlCeConnection cn, ToolStripDropDownButton buttonMenu, String FormName)
        {

            foreach (ToolStripItem item in buttonMenu.DropDownItems){
                using (SqlCeCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        cmd.CommandText = @"
                                INSERT INTO [control_levels]
                                       ([form],[control]
                                       ,[control_level])
                                 select @form,@control,(select authLevel_id from auth_levels where AuthLevel_name =@levelName)
                                 where not exists(select 1 from control_levels where form = @form and control = @control)";

                        if (!String.IsNullOrEmpty(FormName) && !String.IsNullOrEmpty(item.Name))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@form", FormName);
                            cmd.Parameters.AddWithValue("@control", item.Name);
                            int controlHierarchy = 100;
                            if (item.Tag != null)
                            {
                                Int32.TryParse(item.Tag.ToString(), out controlHierarchy);
                            }
                            cmd.Parameters.AddWithValue("@levelName", _userLevels[controlHierarchy > 0 ? controlHierarchy : 100]);

                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = @"
                                update [control_levels]
                                    set [control_level]=(select authLevel_id from auth_levels where AuthLevel_name =@levelName)
                                 where form = @form and control = @control and [control_level]<>(select authLevel_id from auth_levels where AuthLevel_name =@levelName)";

                        if (!String.IsNullOrEmpty(FormName) && !String.IsNullOrEmpty(item.Name))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@form", FormName);
                            cmd.Parameters.AddWithValue("@control", item.Name);
                            int controlHierarchy = 100;
                            if (item.Tag != null)
                            {
                                Int32.TryParse(item.Tag.ToString(), out controlHierarchy);
                            }
                            cmd.Parameters.AddWithValue("@levelName", _userLevels[controlHierarchy > 0 ? controlHierarchy : 100]);

                            cmd.ExecuteNonQuery();
                        }

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }

            }
        }

        private static void ProcessSubMenuItems(SqlCeConnection cn, ToolStripMenuItem menuItemContainer, String FormName)
        {
            foreach (ToolStripDropDownItem item in menuItemContainer.DropDownItems)
            {
                using (SqlCeCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        cmd.CommandText = @"
                                INSERT INTO [control_levels]
                                       ([form],[control]
                                       ,[control_level])
                                 select @form,@control,(select authLevel_id from auth_levels where AuthLevel_name =@levelName)
                                 where not exists(select 1 from control_levels where form = @form and control = @control)";

                        if (!String.IsNullOrEmpty(FormName) && !String.IsNullOrEmpty(item.Name))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@form", FormName);
                            cmd.Parameters.AddWithValue("@control", item.Name);
                            int controlHierarchy = 100;
                            if (item.Tag != null)
                            {
                                Int32.TryParse(item.Tag.ToString(), out controlHierarchy);
                            }
                            cmd.Parameters.AddWithValue("@levelName", _userLevels[controlHierarchy > 0 ? controlHierarchy : 100]);

                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = @"
                                update [control_levels]
                                    set [control_level]=(select authLevel_id from auth_levels where AuthLevel_name =@levelName)
                                 where form = @form and control = @control and [control_level]<>(select authLevel_id from auth_levels where AuthLevel_name =@levelName)";

                        if (!String.IsNullOrEmpty(FormName) && !String.IsNullOrEmpty(item.Name))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@form", FormName);
                            cmd.Parameters.AddWithValue("@control", item.Name);
                            int controlHierarchy = 100;
                            if (item.Tag != null)
                            {
                                Int32.TryParse(item.Tag.ToString(), out controlHierarchy);
                            }
                            cmd.Parameters.AddWithValue("@levelName", _userLevels[controlHierarchy > 0 ? controlHierarchy : 100]);

                            cmd.ExecuteNonQuery();
                        }

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
                if (item.HasDropDownItems)
                {
                    ProcessSubMenuItems(cn, (ToolStripMenuItem)item, FormName);
                }
            }
        }

        public static void InitControls(Control container)
        {
            Dictionary<string, int> formsControls = initFormControlsDictionary(container.Name);
            foreach (Control ctrl in container.Controls)
            {
                if (formsControls.ContainsKey(ctrl.Name))
                {
                    ctrl.Enabled = Program.user.UserHierarchy >= formsControls[ctrl.Name];
                }

                if (ctrl.HasChildren)
                {
                    InitControls(ctrl);
                }

                if (ctrl.GetType().Name.Equals("MenuStrip"))
                {
                    InitMenuControls((MenuStrip)ctrl, formsControls);
                }
                else if (ctrl.GetType().Name.Equals("ToolStrip"))
                {
                    InitToolStripControls((ToolStrip)ctrl, formsControls);
                }
                else if (ctrl.GetType().Name.Equals("TabControl"))
                {
                    InitTabControls((TabControl)ctrl, formsControls);
                }              
            }
        }

        private static void InitMenuControls(MenuStrip menuContainer, Dictionary<string, int> formsControls)
        {
            foreach (ToolStripMenuItem item in menuContainer.Items)
            {
                if (formsControls.ContainsKey(item.Name))
                {
                    item.Enabled = Program.user.UserHierarchy >= formsControls[item.Name];
                }

                if (item.HasDropDownItems)
                {
                    InitSubMenuItems(item, formsControls);
                }
            }
        }

        private static void InitTabControls(TabControl tabControl, Dictionary<string, int> formsControls)
        {
            foreach (TabPage page in tabControl.TabPages)
            {
                if (formsControls.ContainsKey(page.Name))
                {
                    if( Program.user.UserHierarchy < formsControls[page.Name])
                    {
                        tabControl.TabPages.Remove(page);
                    }
                }
            }
        }

        private static void InitToolStripControls(ToolStrip toolStripContainer, Dictionary<string, int> formsControls)
        {
            foreach (ToolStripItem item in toolStripContainer.Items)
            {
                if (formsControls.ContainsKey(item.Name))
                {
                    item.Enabled = Program.user.UserHierarchy >= formsControls[item.Name];
                }
                
                if (item.GetType().Name.Equals("ToolStripDropDownButton"))
                {
                    InitToolStripButtonItems((ToolStripDropDownButton)item, formsControls);
                }
            }
        }

        private static void InitToolStripButtonItems(ToolStripDropDownButton stripButton, Dictionary<string, int> formsControls)
        {
            foreach (ToolStripItem item in stripButton.DropDownItems)
            {
                if (formsControls.ContainsKey(item.Name))
                {
                    item.Enabled = Program.user.UserHierarchy >= formsControls[item.Name];
                }

                //if (item.HasDropDownItems)
                //{
                //    InitSubMenuItems((ToolStripMenuItem)item, formsControls);
                //}
            }
        }

        private static void InitSubMenuItems(ToolStripMenuItem menuItemContainer, Dictionary<string, int> formsControls)
        {
            foreach (ToolStripDropDownItem item in menuItemContainer.DropDownItems)
            {
                if (formsControls.ContainsKey(item.Name))
                {
                    item.Enabled = Program.user.UserHierarchy >= formsControls[item.Name];
                }

                if (item.HasDropDownItems)
                {
                    InitSubMenuItems((ToolStripMenuItem)item, formsControls);
                }
            }
        }

    }
}
