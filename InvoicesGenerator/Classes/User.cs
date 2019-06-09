using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvoicesGenerator
{
    class User
    {
        private string  error = string.Empty;
        private  int _UserHierarchy = 0;
        private  int _UserLevelId = 0;
        private  int? _UserID = null;
        private  int _isActive = 0;
        private  string _username = "";
        private  string _password = "";
        private  string _name = "";
        private  string _surname = "";

        public User()
        {
        }

        public User(int userId, out string error)
        {
            Dictionary<string, object> dr = UserDAL.getUser(userId, out error);
            init(dr);
        }

        public User(Dictionary<string, object> dr)
        {
            init(dr);
        }

        #region Database Fields
        public int IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        public int? UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public int UserHierarchy
        {
            get { return _UserHierarchy; }
            set { _UserHierarchy = value; }
        }

        public int UserLevelId
        {
            get { return _UserLevelId; }
            set { _UserLevelId = value; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string FullName
        {
            get { return _surname+" "+_name; }
        }


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        #endregion
        private void init(Dictionary<string, object> dr)
        {
             _UserHierarchy = Convert.ToInt32(dr["Hierarchy"].ToString());
            _UserLevelId = Convert.ToInt32(dr["user_level_id"].ToString());
            _UserID = Convert.ToInt32(dr["user_id"].ToString());
            _username = dr["username"].ToString();
            _password = dr["password"].ToString();
            _name = dr["name"].ToString();
            _surname = dr["surname"].ToString();
            _isActive = Convert.ToInt32(dr["is_active"].ToString());
        }

        public void setHierarchy(int UserLevelID)
        {
            object hierarchy = UserDAL.getHierarchy(UserLevelID);
            if (hierarchy != null)
            {
                _UserHierarchy = (int)hierarchy;
            }
            else
            {
                _UserHierarchy = -1;
            }
        }

        public Boolean Save()
        {
            if (_UserID.HasValue && _UserID.Value>0)
            {
                return UserDAL.updateUser(this, out error);
            }
            else
            {
                return UserDAL.insert(this, out error);
            }
        }

        public static List<User> getUsers()
        {
            return UserDAL.getUsers();
        }


        public string GetError()
        {
            return error;
        }

      }
}
