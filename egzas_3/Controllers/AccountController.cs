using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using egzas_3.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using egzas_3.Services;
using egzas_3.Services.Interfaces;
using System.Net.Mime;
using egzas_3.Dtos.Requests;
using egzas_3.Mapper.Interfaces;
using egzas_3.Repositories.Interfaces;
using System.Net.Mime;

namespace egzas_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        //private readonly AuthService _authService;
        //private readonly IAccountRepository _repository;
        //private readonly IAccountMapper _mapper;


        //public AccountController(IAccountRepository repository, ApplicationDbContext context, AuthService authService)
        //{
        //    _context = context;
        //    _authService = authService;
        //    _repository = repository;
        //}



        /// <summary>
        /// ////////////
        /// </summary>
        /// <returns></returns>
        /// 
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountRepository _repository;
        private readonly IJwtService _jwtService;
        //private readonly IAccountMapper _mapper;
        private readonly IAccountService _service;
        private readonly ApplicationDbContext _context;


        public AccountController(ILogger<AccountController> logger,
            IAccountRepository repository,
            ApplicationDbContext context,
            IJwtService jwtService,
            /*IAccountMapper mapper,*/
            IAccountService service)
        {
            _logger = logger;
            _repository = repository;
            _jwtService = jwtService;
            //_mapper = mapper;
            _service = service;
            _context = context;
        }




        [HttpGet(Name = "GetAllAccounts")]
        public async Task<ActionResult<IEnumerable<object>>> GetAccounts()
        {
            // Exclude sensitive information like password hash and salt
            return await _context.Accounts
                .Select(a => new
                {
                    a.AccountId,
                    a.AccountName,
                    a.AccountRole
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetAccount(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            // Return account without sensitive data
            return new
            {
                account.AccountId,
                account.AccountName,
                account.AccountRole
            };
        }


        //[HttpPost("SignUp")]
        //[Produces(MediaTypeNames.Application.Json)]
        //[Consumes(MediaTypeNames.Application.Json)]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //public ActionResult SignUp(AccountRequestDto req)
        //{
        //    //_logger.LogInformation($"Creating account for {req.UserName}");
        //    var account = _mapper.Map(req);
        //    var userId = _repository.Create(account);
        //    //_logger.LogInformation($"Account for {req.UserName} created with id {userId}");
        //    return Created("", new { id = userId });
        //}

        //[HttpPost]
        //public async Task<ActionResult<object>> CreateAccount([FromBody] AccountCreateDto accountDto)
        //{
        //    // Validate account name uniqueness
        //    if (await _context.Accounts.AnyAsync(a => a.AccountName == accountDto.AccountName))
        //    {
        //        return Conflict("An account with this name already exists.");
        //    }

        //    // Create password hash and salt
        //    CreatePasswordHash(accountDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        //    var account = new Account
        //    {
        //        AccountId = Guid.NewGuid(),
        //        AccountName = accountDto.AccountName,
        //        AccountEmail = accountDto.AccountEmail,
        //        AccountPasswordHash = passwordHash,
        //        AccountPasswordSalt = passwordSalt,
        //        AccountRole = accountDto.AccountRole ?? "user"
        //    };

        //    _context.Accounts.Add(account);
        //    await _context.SaveChangesAsync();

        //    // Return created account without sensitive data
        //    return CreatedAtAction(nameof(GetAccount), new { id = account.AccountId }, new
        //    {
        //        account.AccountId,
        //        account.AccountName,
        //        account.AccountEmail,
        //        account.AccountRole
        //    });
        //}

        [HttpPost]
        public async Task<ActionResult<object>> CreateAccount([FromBody] AccountCreateDto accountDto)
        {
            // Check if the email is missing
            if (string.IsNullOrWhiteSpace(accountDto.AccountEmail))
            {
                return BadRequest(new { Error = "The AccountEmail field is required." });
            }

            // Validate account name uniqueness
            if (await _context.Accounts.AnyAsync(a => a.AccountName == accountDto.AccountName))
            {
                return Conflict("An account with this name already exists.");
            }

            // Create password hash and salt
            CreatePasswordHash(accountDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var account = new Account
            {
                AccountId = Guid.NewGuid(),
                AccountName = accountDto.AccountName,
                AccountEmail = accountDto.AccountEmail,
                AccountPasswordHash = passwordHash,
                AccountPasswordSalt = passwordSalt,
                AccountRole = accountDto.AccountRole ?? "user"
            };

            // Add to database and save
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            // Return created account without sensitive data
            return CreatedAtAction(nameof(GetAccount), new { id = account.AccountId }, new
            {
                account.AccountId,
                account.AccountName,
                account.AccountEmail,
                account.AccountRole
            });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(Guid id, [FromBody] AccountUpdateDto accountDto)
        {
            var existingAccount = await _context.Accounts.FindAsync(id);
            if (existingAccount == null)
            {
                return NotFound();
            }

            // Check for name conflict with other accounts
            if (await _context.Accounts.AnyAsync(a => a.AccountName == accountDto.AccountName && a.AccountId != id))
            {
                return Conflict("An account with this name already exists.");
            }

            // Update only specific fields
            existingAccount.AccountName = accountDto.AccountName;

            // Only update role if provided
            if (!string.IsNullOrWhiteSpace(accountDto.AccountRole))
            {
                existingAccount.AccountRole = accountDto.AccountRole;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(Guid id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }

        // Password hashing method matching your initial implementation
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA256();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        // Data Transfer Objects for input validation
        public class AccountCreateDto
        {
            public string AccountName { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string? AccountRole { get; set; }
            public string AccountEmail { get; set; } = string.Empty;
        } 

        public class AccountUpdateDto
        {
            public string AccountName { get; set; } = string.Empty;
            public string? AccountRole { get; set; }
        }
    }
}