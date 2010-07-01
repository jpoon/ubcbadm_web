using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace ubcbadm
{
    [DataContract]
    public class ClubMember : INotifyPropertyChanged, IDataErrorInfo
    {
        #region Properties

        [DataMember]
        public string firstName
        {
            get { return _firstName; }
            set
            {
                value = TitleCase(value);
                if (value != _firstName)
                {
                    _firstName = value;
                    InvokePropertyChanged("firstName");
                }
            }
        }
        private string _firstName = null;

        [DataMember]
        public string lastName
        {
            get { return _lastName; }
            set
            {
                value = TitleCase(value);
                if (value != _lastName)
                {
                    _lastName = value;
                    InvokePropertyChanged("lastName");
                }
            }
        }
        private string _lastName = null;

        [DataMember]
        public string affiliation
        {
            get { return _affiliation; }
            set
            {
                if (value != _affiliation)
                {
                    _affiliation = value;
                    InvokePropertyChanged("lastName");
                }
            }
        }
        private string _affiliation = "student";

        [DataMember]
        public string studentNo
        {
            get { return _studentNo; }
            set
            {
                if (value != _studentNo)
                {
                    _studentNo = value;
                    InvokePropertyChanged("studentNo");
                }
            }
        }
        private string _studentNo;

        [DataMember]
        public string phoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                if (value != _phoneNumber)
                {
                    _phoneNumber = value;
                    InvokePropertyChanged("phoneNumber");
                }
            }
        }
        private string _phoneNumber;

        [DataMember]
        public string email
        {
            get
            {
                return _email;
            }
            set
            {
                if (value != _email)
                {
                    _email = value;
                    InvokePropertyChanged("email");
                }
            }
        }
        private string _email;

        [DataMember]
        public string memberType
        {
            get
            {
                return _memberType;
            }
            set
            {
                if (value != _memberType)
                {
                    _memberType = value;
                    InvokePropertyChanged("memberType");
                }
            }
        }
        private string _memberType;

        [DataMember]
        public string skillLevel
        {
            get
            {
                return _skillLevel;
            }
            set
            {
                if (value != _skillLevel)
                {
                    _skillLevel = value;
                    InvokePropertyChanged("skillLevel");
                }
            }
        }
        private string _skillLevel;

        public string TitleCase(string str)
        {
            return Regex.Replace(str, @"\w+", (m) =>
            {
                string tmp = m.Value;
                return char.ToUpper(tmp[0]) + tmp.Substring(1, tmp.Length - 1).ToLower();
            });
        }

        #endregion

        #region Json
        public static string Serialize(object objectToSerialize)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractJsonSerializer serializer =
                        new DataContractJsonSerializer(objectToSerialize.GetType());

                serializer.WriteObject(ms, objectToSerialize);
                ms.Position = 0;

                using (StreamReader reader = new StreamReader(ms))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static T Deserialize<T>(Stream jsonStream)
        {
            DataContractJsonSerializer serializer =
                new DataContractJsonSerializer(typeof(T));
            return (T)serializer.ReadObject(jsonStream);
        }

        #endregion

        #region ErrorHandling

        public string Error
        {
            get { return (null); }
        }

        public string this[string columnName]
        {
            get
            {
                string error = null;

                switch (columnName)
                {
                    case "firstName":
                        if (string.IsNullOrEmpty(firstName))
                        {
                            error = "Provide a first name";
                        }
                        break;
                    case "lastName":
                        if (string.IsNullOrEmpty(lastName))
                        {
                            error = "Provide a last name";
                        }
                        break;
                    case "studentNo":
                        if (!string.IsNullOrEmpty(studentNo))
                        {
                            if (!Regex.IsMatch(studentNo, @"^[0-9]+$"))
                            {
                                error = "Student number should be composed of numerical digits only";
                            }
                            else if (studentNo.Length != 8)
                            {
                                error = "Student number should be 8 digits long";
                            }
                        }
                        break;
                    case "phoneNumber":
                        if (string.IsNullOrEmpty(phoneNumber))
                        {
                            error = "Provide an phone number";
                        }
                        else if (!Regex.IsMatch(phoneNumber, @"^[0-9\-\(\)]+$"))
                        {
                            error = "Invalid phone number \n(ex. 604-123-1234)";
                        }
                        break;
                    case "email":
                        if (string.IsNullOrEmpty(email))
                        {
                            error = "Provide an email";
                        }
                        else if (!Regex.IsMatch(email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                        {
                            error = "Invalid email provided \n(ex. john@emailprovider.com)";
                        }
                        break;
                }
                return (error);
            }
        }

        public bool NoErrors
        {
            get
            {
                return (errorCount == 0);
            }
        }

        int errorCount;
        public void BindingValidationError(object sender, ValidationErrorEventArgs e)
        {
            switch (e.Action)
            {
                case ValidationErrorEventAction.Added:
                    errorCount++;
                    break;
                case ValidationErrorEventAction.Removed:
                    errorCount--;
                    break;
                default:
                    break;
            }
            InvokePropertyChanged("NoErrors");
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
