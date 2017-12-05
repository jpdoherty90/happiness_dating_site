using Xunit;
using Moq;
using Match.Controllers;
using Match.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace Match
{

    public class HomeTests
    {

        [Fact]
        public void testLogin() {

            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseNpgsql(connectionString: "DBInfo:ConnectionString");

            var _context = new Context(optionsBuilder.Options);

            User johnSmith = _context.Users.SingleOrDefault(user => user.email == "john@smith.com");
            Assert.Equal(johnSmith, null);

            //HomeController controller = new HomeController(_context);

            

        }

    }

}