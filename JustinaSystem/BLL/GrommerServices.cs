using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JustinaSystem.DAL;
using JustinaSystem.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace JustinaSystem.BLL
{
    public class GrommerServices
    {
        private readonly JustinaContext _justinaContext;

        internal GrommerServices(JustinaContext justinaContext)
        {
            
            _justinaContext = justinaContext;
        }

        public async Task<List<GroomerView>> ViewAllGroomers()
        {
            List<GroomerView> groomers = new List<GroomerView>();
            try
            {
                groomers = await _justinaContext.Groomers
                    .Select(g => new GroomerView
                    {
                        GroomerId = g.groomer_id,
                        FirstName = g.first_name,
                        LastName = g.last_name,
                        PhoneNumber = g.phone_number,
                        EmailAddress = g.email_address,
                        EnrollmentDate = g.enrollment_date,
                        Notes = g.notes
                    }).OrderBy(x => x.EnrollmentDate).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GroomerServices.ViewGroomers: " + ex.Message);
            }
            return groomers;
        }
    }
}
