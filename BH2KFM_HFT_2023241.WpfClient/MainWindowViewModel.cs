using BH2KFM_HFT_2023241.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BH2KFM_HFT_2023241.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        //REST API setup
        public static RestService rest;
        public static int PortNumber = 60321;


        //Subject properties
        #region Subjects
        public RestCollection<Subject> Subjects { get; set; }

        private Subject selectedSubject;
        public Subject SelectedSubject
        {
            get { return selectedSubject; }
            set
            {
                if(value != null)
                {
                    selectedSubject = new Subject()
                    {
                        Name = value.Name,
                        SubjectID = value.SubjectID
                    };
                    OnPropertyChanged();
                    //SetProperty(ref selectedSubject,value);
                    (DeleteSubjectCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        private double averageCreditValue;
        public double AverageCreditValue
        {
            get { return averageCreditValue; }
            set { averageCreditValue = value; OnPropertyChanged(); }
        }

        private int mostCreditSemester;
        public int MostCreditSemester
        {
            get { return mostCreditSemester; }
            set { mostCreditSemester = value; OnPropertyChanged(); }
        }


        public ICommand CreateSubjectCommand { get; set; }
        public ICommand DeleteSubjectCommand { get; set; }
        public ICommand UpdateSubjectCommand { get; set; }

        #endregion Subjects

        //Room properties
        #region Rooms
        public RestCollection<Room> Rooms { get; set; }

        private Room selectedRoom;
        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                if (value != null)
                {
                    selectedRoom = new Room()
                    {
                        Capacity = value.Capacity,
                        DoorID = value.DoorID
                    };
                    OnPropertyChanged();
                    (DeleteSubjectCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        private int maxCapacity;
        public int MaxCapacity
        {
            get { return maxCapacity; }
            set { maxCapacity = value; OnPropertyChanged(); }
        }


        public ICommand CreateRoomCommand { get; set; }
        public ICommand DeleteRoomCommand { get; set; }
        public ICommand UpdateRoomCommand { get; set; }
        #endregion Rooms

        //Course properties
        #region Courses
        public RestCollection<Course> Courses { get; set; }

        private Course selectedCourse;
        public Course SelectedCourse
        {
            get { return selectedCourse; }
            set
            {
                if (value != null)
                {
                    selectedCourse = new Course()
                    {
                        Location = value.Location,
                        CourseID = value.CourseID
                    };
                    OnPropertyChanged();
                    (DeleteSubjectCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        private double averageCourseLength;
        public double AverageCourseLength
        {
            get { return averageCourseLength; }
            set { averageCourseLength = value; OnPropertyChanged(); }
        }

        private int maxCourseLength;
        public int MaxCourseLength
        {
            get { return maxCourseLength; }
            set { maxCourseLength = value; OnPropertyChanged(); }
        }

        private bool anyOverlapping;
        public bool AnyOverlapping
        {
            get { return anyOverlapping; }
            set { anyOverlapping = value; OnPropertyChanged(); }
        }


        public ICommand CreateCourseCommand { get; set; }
        public ICommand DeleteCourseCommand { get; set; }
        public ICommand UpdateCourseCommand { get; set; }
        #endregion Courses


        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                rest = new RestService($"http://localhost:{PortNumber}/");

                //Initial setup for subjects
                #region SubjectSetup
                Subjects = new RestCollection<Subject>($"http://localhost:{PortNumber}/","Subject","hub");

                CreateSubjectCommand = new RelayCommand(() =>
                {
                    Subjects.Add(new Subject(){ Name = SelectedSubject.Name });
                });

                DeleteSubjectCommand = new RelayCommand(() =>
                {
                    Subjects.Delete(SelectedSubject.SubjectID);
                }, () => SelectedSubject != null);

                UpdateSubjectCommand = new RelayCommand(() =>
                {
                    Subjects.Update(SelectedSubject);
                });

                selectedSubject = new Subject();

                Subjects.CollectionChanged += (sender, e) =>
                {
                    AverageCreditValue = Math.Round(rest.GetSingle<double>("SubjectStat/AverageCreditValue"),2);
                    MostCreditSemester = rest.GetSingle<int>("SubjectStat/MostCreditSemester");
                };
                #endregion SubjectSetup


                //Initial setup for rooms
                #region RoomSetup
                Rooms = new RestCollection<Room>($"http://localhost:{PortNumber}/", "Room","hub");

                CreateRoomCommand = new RelayCommand(() =>
                {
                    Rooms.Add(new Room() { Capacity = SelectedRoom.Capacity });
                });

                DeleteRoomCommand = new RelayCommand(() =>
                {
                    Rooms.Delete(SelectedRoom.DoorID);
                }, () => SelectedRoom != null);

                UpdateRoomCommand = new RelayCommand(() =>
                {
                    Rooms.Update(SelectedRoom);
                });

                selectedRoom = new Room();

                Rooms.CollectionChanged += (sender, e) =>
                {
                    MaxCapacity = rest.GetSingle<int>("RoomStat/MaxCapacity");
                };
                #endregion RoomSetup


                //Initial setup for courses
                #region CourseSetup
                Courses = new RestCollection<Course>($"http://localhost:{PortNumber}/", "Course","hub");

                CreateCourseCommand = new RelayCommand(() =>
                {
                    Courses.Add(new Course() { Location = SelectedCourse.Location });
                });

                DeleteCourseCommand = new RelayCommand(() =>
                {
                    Courses.Delete(SelectedCourse.CourseID);
                }, () => SelectedCourse != null);

                UpdateCourseCommand = new RelayCommand(() =>
                {
                    Courses.Update(SelectedCourse);
                });

                selectedCourse = new Course();

                Courses.CollectionChanged += (sender, e) =>
                {
                    AverageCourseLength = rest.GetSingle<double>("CourseStat/AverageCourseLengthMinutes");
                    MaxCourseLength = rest.GetSingle<int>("CourseStat/MaxCourseLengthMinutes");
                    AnyOverlapping = rest.GetSingle<bool>("CourseStat/AnyOverlapping");
                };
                #endregion CourseSetup
            }
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
    }
}
