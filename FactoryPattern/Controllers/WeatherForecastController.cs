using Microsoft.AspNetCore.Mvc;

namespace FactoryPattern.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IAbstractFactory<ISample1> _sampleFactory;
        private readonly IUserDataFactory _userDataFactory;
        private readonly IDriverFactory<Car> _driverFactory;
        public WeatherForecastController(
            IAbstractFactory<ISample1> sampleFactory,
            IUserDataFactory userDataFactory,
            IDriverFactory<Car> driverFactory)
        {
            _sampleFactory = sampleFactory;
            _userDataFactory = userDataFactory;
            _driverFactory = driverFactory;
        }

        [HttpGet("GetDateTime")]
        public IActionResult GetDateTime()
        {
            var sample1 = _sampleFactory.Create();
            var dateTime = sample1.GetDateTime();
            return Ok(dateTime);
        }

        [HttpGet("GetUserData")]
        public IActionResult GetUserData()
        {
            var userData = _userDataFactory.GetUserData("Doni");
            return Ok(userData);
        }

        [HttpGet("GetDriverData")]
        public IActionResult GetDriverData()
        {
            var info = _driverFactory.Start();
            return Ok(info);
        }
    }
}