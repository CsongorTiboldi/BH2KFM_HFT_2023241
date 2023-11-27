using BH2KFM_HFT_2023241.Logic;
using Microsoft.AspNetCore.Mvc;

namespace BH2KFM_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CourseStatController : ControllerBase
    {
        ICourseLogic logic;

        public CourseStatController(ICourseLogic logic)
        {
            this.logic = logic;
        }
        
        [HttpGet("{course}")]
        public int CourseLengthMinutes(int course)
        {
            return this.logic.CourseLengthMinutes(course);
        }
        
        [HttpGet]
        public double AverageCourseLengthMinutes()
        {
            return this.logic.AverageCourseLengthMinutes();
        }
        
        [HttpGet]
        public int MaxCourseLengthMinutes()
        {
            return this.logic.MaxCourseLengthMinutes();
        }
        
        [HttpGet]
        public bool AreOverlapping(int course1, int course2)
        {
            return this.logic.AreOverlapping(course1, course2);
        }
        
        [HttpGet]
        public bool AnyOverlapping()
        {
            return this.logic.AnyOverlapping();
        }
    }
}
