using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LetsBank.Core.Entities;
using LetsBank.Core.Models;
using LetsBank.Core.Services;
using LetsBank.Infrastructure.Utilities;
using LetsBank.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LetsBank.Web.Controllers
{
	/// <summary>
	///				LetsBank operations for WebApp
	/// </summary>
	[Authorize]
	public class AccountManagementController : Controller
    {
		private readonly UserManager<IdentityUser>				_userManager;
		private readonly IAccountManagementService				_accountManagementService;
		private readonly IMapper								_mapper;
		private readonly ILogger<AccountManagementController>	_logger;

		private Guid currentUserId = Guid.Empty;

		public AccountManagementController(
			UserManager<IdentityUser>			userManager,
			IAccountManagementService			accountManagementService,
			IMapper								mapper,
			ILogger<AccountManagementController>logger)
		{
			_userManager				= userManager;
			_accountManagementService	= accountManagementService;
			_mapper						= mapper;
			_logger						= logger;
		}

		[HttpPost]
		public IActionResult RecordDeposit(string amount)
		{
			try
			{
				decimal amountDecimal;
				if (decimal.TryParse(amount, out amountDecimal))
				{
					return Json(
						new
						{
							result = _accountManagementService.RecordDeposit(LoggedInUserBankAccount(), LoggedInUserId(), amountDecimal)
						}
					);
				}
				else
				{
					return BadRequest();
				}
			}
			catch (Exception e)
			{
				_logger.LogError("ERROR[RecordDeposit] ::" + e.ToString());
				return StatusCode(500);
			}
		}

		[HttpPost]
		public IActionResult RecordWithdrawal(string amount)
		{
			try
			{
				decimal amountDecimal;
				if (decimal.TryParse(amount, out amountDecimal))
				{
					return Json(
						new
						{
							result = _accountManagementService.RecordWithdrawal(LoggedInUserBankAccount(), LoggedInUserId(), amountDecimal)
						}
					);
				}
				else
				{
					return BadRequest();
				}
			}
			catch (Exception e)
			{
				_logger.LogError("ERROR[RecordWithdrawal] ::" + e.ToString());
				return StatusCode(500);
			}
		}

		[HttpGet]
		public IActionResult AccountBalance()
		{
			try
			{
				return Json(
					new
					{
						balance = _accountManagementService.CheckBalance(LoggedInUserId())
					}
				);
			}
			catch (Exception e)
			{

				_logger.LogError("ERROR[AccountBalance] ::" + e.ToString());
				return StatusCode(500);
			}
		}

		[HttpGet]
		public IActionResult TransactionHistory()
		{
			try
			{
				return Json(new
				{
					txns = _mapper.Map<IList<TxnRecordModel>>(

						_accountManagementService.GetTransactionHistory(LoggedInUserId())
					)
				});
			}
			catch (Exception e)
			{
				_logger.LogError("ERROR[TransactionHistory] ::" + e.ToString());
				return StatusCode(500);
			}
		}


		/* Helper methods */

		private Account LoggedInUserBankAccount()
		{
			return _accountManagementService.GetAccountByUserId(LoggedInUserId());
		}
		private Guid LoggedInUserId()
		{
			if (currentUserId == Guid.Empty)
			{
				currentUserId = LoggedInUserAccountIdAsync().Result;
			}

			return currentUserId;
		}
		private async Task<Guid> LoggedInUserAccountIdAsync()
		{
			var userName = HttpContext.User.Identity.Name;
			var user = await _userManager.FindByNameAsync(userName);

			return StringUtils.StringToGUID(user.Email); ;
		}
	}
}