using System;
using AutoMapper;
using KNC.Helper;
using KNC.Models;
using KNC.Services;
using Unity;
using Unity.AspNet.Mvc;


namespace KNC
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        public static void RegisterTypes(IUnityContainer container)
        {
            // Register DbContext with per-request lifetime
            container.RegisterType<ApplicationDbContext>(new PerRequestLifetimeManager());

            // Register AutoMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            container.RegisterInstance(mapperConfig);
            container.RegisterInstance<IMapper>(mapperConfig.CreateMapper());

            // Register services with per-request lifetime
            container.RegisterType<StudentService>(new PerRequestLifetimeManager());
            container.RegisterType<ProgramsService>(new PerRequestLifetimeManager());
            container.RegisterType<FacultyService>(new PerRequestLifetimeManager());
            container.RegisterType<ProgramCourseService>(new PerRequestLifetimeManager());
            container.RegisterType<RoutineService>(new PerRequestLifetimeManager());
            container.RegisterType<DesignationService>(new PerRequestLifetimeManager());
            container.RegisterType<DropDownService>(new PerRequestLifetimeManager());
            container.RegisterType<StudentEnrollmentService>(new PerRequestLifetimeManager());
            container.RegisterType<StudentFeeService>(new PerRequestLifetimeManager());
            container.RegisterType<MonthlyFeeService>(new PerRequestLifetimeManager());
            container.RegisterType<CourseTeacherAssignmentService>(new PerRequestLifetimeManager());
            container.RegisterType<CoursesService>(new PerRequestLifetimeManager());
        }
    }
}