﻿using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using System.Windows.Controls;

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
                value = Util.TitleCase(value);
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
                value = Util.TitleCase(value);
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
                    InvokePropertyChanged("affiliation");

                    /* Notify error validation of updated affiliation value */
                    InvokePropertyChanged("studentNo");
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
        private string _memberType = "new";

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
        private string _skillLevel = "beginner";

        #endregion

        #region Radio_Buttons
        /* Affiliation */
        public bool isAffiliation_student
        {
            get { return affiliation == "student"; }
            set
            {
                if (value == true)
                    affiliation = "student";
            }
        }

        public bool isAffiliation_faculty
        {
            get { return affiliation == "faculty"; }
            set
            {
                if (value == true)
                    affiliation = "faculty";
            }
        }

        public bool isAffiliation_other
        {
            get { return affiliation == "other"; }
            set
            {
                if (value == true)
                    affiliation = "other";
            }
        }

        /* Member Type */
        public bool isMemberType_New
        {
            get { return memberType == "new"; }
            set
            {
                if (value == true)
                    memberType = "new";
            }
        }

        public bool isMemberType_Returning
        {
            get { return memberType == "returning"; }
            set
            {
                if (value == true)
                    memberType = "returning";
            }
        }

        /* Skill Level */
        public bool isSkillLevel_Beginner
        {
            get { return skillLevel == "beginner"; }
            set
            {
                if (value == true)
                    skillLevel = "beginner";
            }
        }

        public bool isSkillLevel_Novice
        {
            get { return skillLevel == "novice"; }
            set
            {
                if (value == true)
                    skillLevel = "novice";
            }
        }

        public bool isSkillLevel_Intermediate
        {
            get { return skillLevel == "intermediate"; }
            set
            {
                if (value == true)
                    skillLevel = "intermediate";
            }
        }

        public bool isSkillLevel_Advanced
        {
            get { return skillLevel == "advanced"; }
            set
            {
                if (value == true)
                    skillLevel = "advanced";
            }
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

        #region Data_Validation

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
                        if (affiliation == "student")
                        {
                            if (string.IsNullOrEmpty(studentNo))
                            {
                                error = "Provide a student number";
                            }
                            else if (!Regex.IsMatch(studentNo, @"^[0-9]+$"))
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
                            error = "Provide an phone number \n(ex. 604-123-1234)";
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
