using AutoMapper;
using KNC.Helper;
using KNC.Models;
using KNC.Services;
using System;
using Unity;
using Unity.Injection;

namespace KNC
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        [Obsolete]
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
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        [Obsolete]
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
                // Add other profiles here if needed
            });

            // Register IMapper in Unity, injecting the MapperConfiguration
            container.RegisterInstance(mapperConfig); // singleton MapperConfiguration

            // Register IMapper using factory method
            container.RegisterType<IMapper>(new InjectionFactory(c =>
            {
                var config = c.Resolve<MapperConfiguration>();
                return config.CreateMapper();
            }));

            // Register DbContext
            container.RegisterType<ApplicationDbContext>();

            // Register services
            container.RegisterType<StudentService>();
            container.RegisterType<FacultyService>();
            container.RegisterType<ProgramCourseService>();
            container.RegisterType<RoutineService>();
            container.RegisterType<DesignationService>();
            container.RegisterType<DropDownService>();
            container.RegisterType<ProgramsService>();
            container.RegisterType<StudentEnrollmentService>();
            container.RegisterType<StudentFeeService>();
            container.RegisterType<MonthlyFeeService>();
            container.RegisterType<CourseTeacherAssignmentService>();
            container.RegisterType<CoursesService>();
        }
    }
}