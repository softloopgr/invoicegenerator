using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;
using System.Configuration;
using System.Transactions;

namespace InvoicesGenerator
{
    static class UserDAL
    {
        public static List<User> getUsers()
        {
            List<User> Users = new List<User>();
            try
            {

                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"SELECT  u.user_id, 
                                                u.name,
                                                u.surname, 
                                                u.username,
                                                u.password,
                                                ul.user_level_id,
                                                al.hierarchy,
                                                u.is_active
                                        FROM Users u
                                        INNER JOIN user_levels ul on ul.user_id=u.user_id 
                                        inner join auth_levels al on al.AuthLevel_id =ul.user_level_id
                                        WHERE u.is_active = 1";

                        cn.Open();
                        using (SqlCeDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Dictionary<string, object> row = new Dictionary<string, object>();
                                for (int i = 0; i <= dr.FieldCount - 1; i++)
                                {
                                    try
                                    {
                                        row.Add(dr.GetName(i), dr[i]);
                                    }
                                    catch { }
                                }
                                string error = string.Empty;
                                Users.Add(new User(row));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return Users;
        }

        public static Dictionary<string, object> getUser(int UserId, out string error)
        {
            Dictionary<string, object> row = new Dictionary<string, object>();
            error = string.Empty;
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"SELECT  u.user_id, 
                                                u.name,
                                                u.surname, 
                                                u.username,
                                                u.password,
                                                ul.user_level_id,
                                                al.hierarchy,
                                                is_active
                                        FROM users u
                                        INNER JOIN user_levels ul on ul.user_id=u.user_id 
                                        inner join auth_levels al on al.AuthLevel_id =ul.user_level_id
                                        WHERE u.is_active = 1 and u.user_id =@User_id";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@User_id", UserId);

                        cn.Open();
                        using (SqlCeDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                for (int i = 0; i <= dr.FieldCount - 1; i++)
                                {

                                    row.Add(dr.GetName(i), dr[i]);

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
                error = e.Message;
            }
            return row;
        }

        public static bool insert(User user, out string error)
        {
            error = string.Empty;
            //try
            //{
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    cn.Open();
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        try
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = @"INSERT INTO users
                                                       ([name],[surname],[username],[password],[is_active])
                                                        VALUES
                                                       (@name,@surname,@username,@password,@is_active)";

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@name", user.Name);
                            cmd.Parameters.AddWithValue("@surname", user.Surname);
                            cmd.Parameters.AddWithValue("@username", user.Username);
                            cmd.Parameters.AddWithValue("@password", user.Password);
                            cmd.Parameters.AddWithValue("@is_active", user.IsActive);
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = @"select @@identity from users";
                            cmd.Parameters.Clear();
                            user.UserID = int.Parse(cmd.ExecuteScalar().ToString());

                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = @"INSERT INTO [user_levels]
                                                           ([user_id],[user_level_id])
                                                     VALUES
                                                           (@user_id,@user_level_id)";

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@user_id", user.UserID);
                            cmd.Parameters.AddWithValue("@user_level_id", user.UserLevelId);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
                            error = e.Message;
                            return false;
                        }
                    }
                }
                return true;
            //}
            //catch (Exception e)
            //{
            //    SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
            //    error = e.Message;
            //    return false;
            //}
        }

        public static Boolean updateUser(User user, out string error)
        {
            error = string.Empty;
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    cn.Open();
                    using (TransactionScope scope = new TransactionScope())
                    {
                        using (SqlCeCommand cmd = cn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            try
                            {
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = @"Update [users]
                                                     set [name]=@name
                                                        ,[surname]=@surname
                                                        ,[username]=@username
                                                        ,[password]=@password
                                                        ,[is_active]=@is_active
                                                where user_id=@user_id";

                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@name", user.Name);
                                cmd.Parameters.AddWithValue("@surname", user.Surname);
                                cmd.Parameters.AddWithValue("@username", user.Username);
                                cmd.Parameters.AddWithValue("@password", user.Password);
                                cmd.Parameters.AddWithValue("@is_active", user.IsActive);
                                cmd.Parameters.AddWithValue("@user_id", user.UserID);
                                cmd.ExecuteNonQuery();

                                cmd.Parameters.Clear();

                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = @"Update [user_levels]
                                                           set [user_level_id] = @user_level_id
                                                     where user_id=@user_id";

                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@user_id", user.UserID);
                                cmd.Parameters.AddWithValue("@user_level_id", user.UserLevelId);
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception e)
                            {
                                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
                                error = e.Message;
                                return false;
                            }
                        }
                        scope.Complete();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
                error = e.Message;
                return false;
            }
        }

        public static Boolean deleteUser(User user, out string error)
        {
            error = string.Empty;
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
                        UPDATE Users 
                        SET is_active=0
                        WHERE user_id=@UserID";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@UserID", user.UserID);

                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
                error = "Κωδικος: 001\n" + e.Message;
                return false;
            }
        }

        public static List<KeyValuePair<int, string>> getLevelCombo()
        {
            List<KeyValuePair<int, string>> levels = new List<KeyValuePair<int, string>>();
            using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
            {
                cn.Open();
                using (SqlCeCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        cmd.CommandText = @"Select AuthLevel_id, AuthLevel_name from auth_levels";

                        cmd.Parameters.Clear();
                        using (SqlCeDataReader levelReader = cmd.ExecuteReader())
                        {
                            while (levelReader.Read())
                            {
                                levels.Add(new KeyValuePair<int, string>(levelReader.GetInt32(levelReader.GetOrdinal("AuthLevel_id")), levelReader.GetString(levelReader.GetOrdinal("AuthLevel_name"))));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        General.ErrorMessage(e.Message);
                    }
                }
            }
            return levels;
        }

        public static object getHierarchy(int LevelID)
        {
            object result = null;
            using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
            {
                cn.Open();
                using (SqlCeCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        cmd.CommandText = @"Select Hierarchy from auth_levels where AuthLevel_id=@AuthLevel_id order by Hierarchy desc";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@AuthLevel_id", LevelID);
                        result = cmd.ExecuteScalar();
                    }
                    catch (Exception e)
                    {
                        General.ErrorMessage(e.Message);
                    }
                }
            }
            return result;
        }

        public static int? getCurrentUserID()
        {
            return Program.user.UserID;
        }

        public static string getUsername(int UserID)
        {
            string UserName = string.Empty;
            try
            {
                using (SqlCeConnection cn = new SqlCeConnection(Program.connectionString))
                {
                    using (SqlCeCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"SELECT
                                                u.username
                                        FROM [Users] u
                                        WHERE u.user_id =@User_id";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@User_id", UserID);

                        cn.Open();
                        using (SqlCeDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                UserName = dr["username"].ToString();

                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SoftloopTools.ExceptionLog(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return UserName;
        }
    }
}






