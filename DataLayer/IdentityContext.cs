using BusinessLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace DataLayer
{
	public class IdentityContext
	{

		UserManager<User> userManager;
		SignInManager<User> signInManager;
		VotingDBContext context;

		public IdentityContext(VotingDBContext context,
			UserManager<User> userManager,
			SignInManager<User> signInManager)
		{
			this.context = context;
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		#region Seeding Data with this Project

		public async Task SeedDataAsync(string adminPass, string adminEmail)
		{
			//await context.Database.MigrateAsync();

			int roles = await context.Roles.CountAsync();

			if (roles == 0)
			{
				await ConfigureRolesAsync();
			}

			await ConfigureAdminAccountAsync(adminPass, adminEmail);
		}

		private async Task ConfigureRolesAsync()
		{
			// NormalizedName is not set by Identity :[ but when you want to find role by its name
			// remember that you are checking Normalized column's value! That is why I am adding NormalizedName value!
			IdentityRole admin = new IdentityRole(Role.Admin.ToString()) { NormalizedName = Role.Admin.ToString().ToUpper() };
			IdentityRole user = new IdentityRole(Role.User.ToString()) { NormalizedName = Role.User.ToString().ToUpper() };
			IdentityRole unspecified = new IdentityRole(Role.Unspecified.ToString()) { NormalizedName = Role.Unspecified.ToString().ToUpper() };

			context.Roles.Add(admin);
			context.Roles.Add(user);
			context.Roles.Add(unspecified);
			await context.SaveChangesAsync();
		}

		private async Task ConfigureAdminAccountAsync(string password, string email)
		{
			User adminIdentityUser;
			// IDCard iDCard = new IDCard("1", "admin");
			adminIdentityUser = new User(email);
			//   IDCardContext idcontext = new IDCardContext();
			//adminIdentityUser.PasswordHash = "admin";
			adminIdentityUser.UserName = email;
			adminIdentityUser.Authenticated = true;
			context.Users.Add(adminIdentityUser);
			await context.SaveChangesAsync();

			await userManager.AddToRoleAsync(adminIdentityUser, Role.Admin.ToString());
			await userManager.AddPasswordAsync(adminIdentityUser, password);
			await userManager.SetEmailAsync(adminIdentityUser, email);

		}

		#endregion

		#region CRUD

		public async Task CreateUserAsync(User user, Role role)
		{
			try
			{
				IdentityResult result = await userManager.CreateAsync(user, user.PasswordHash);

				if (!result.Succeeded)
				{
					throw new ArgumentException(result.Errors.First().Description);
				}
				else
				{
					User photoUser = await context.Users.Include(u => u.FK_IDCard)
				   .ThenInclude(card => card.CardPhotos)
				   .FirstOrDefaultAsync(u => u.Id == user.Id);
					photoUser.FK_IDCard.CardPhotos.IDCardImage1 = user.FK_IDCard.CardPhotos.IDCardImage1;
					photoUser.FK_IDCard.CardPhotos.IDCardImage2 = user.FK_IDCard.CardPhotos.IDCardImage2;

					context.Users.Update(photoUser);
					await context.SaveChangesAsync();

					IdentityResult updRes = await userManager.UpdateAsync(user);
					if (!updRes.Succeeded)
					{
						throw new ArgumentException(result.Errors.First().Description);
					}
				}

				if (role == Role.Admin)
				{
					await userManager.AddToRoleAsync(user, Role.Admin.ToString());
				}
				else
				{
					await userManager.AddToRoleAsync(user, Role.User.ToString());
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<ClaimsPrincipal> LogInUserAsync(string egn, string password)
		{
			try
			{
				User user = await ReadUserEGNAsync(egn);

				if (user == null)
				{
					return null;
				}

				bool result = await userManager.CheckPasswordAsync(user, password); 

				if (result)
				{
					User loggedUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == egn);

					return await signInManager.CreateUserPrincipalAsync(loggedUser);
				}
				else
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<ClaimsPrincipal> LogOutUserAsync(ClaimsPrincipal claimsPrincipal)
		{
			if (claimsPrincipal.Identity is not null && claimsPrincipal.Identity.IsAuthenticated)
			{
				return new ClaimsPrincipal();
			}

			// If should always be true when you call this method!
			return claimsPrincipal;
		}

		public async Task<User> ReadUserAsync(string key, bool useNavigationalProperties = false, bool photoes = false)
		{
			try
			{
				if (photoes)
				{
					return await userManager.Users.Include(u => u.ElectionsVoted)
				   .Include(u => u.FK_IDCard)
				   .ThenInclude(card => card.CardPhotos)
				   .FirstOrDefaultAsync(u => u.Id == key);
				}
				else if (useNavigationalProperties)
				{
					return await userManager.Users.Include(u => u.ElectionsVoted)
						.Include(u => u.FK_IDCard)
						.FirstOrDefaultAsync(u => u.Id == key);

				}

				return await userManager.FindByIdAsync(key);
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<User> ReadUserEGNAsync(string egn, bool useNavigationalProperties = false, bool photoes = false)
		{
			try
			{
				if (photoes)
				{
					return await userManager.Users.Include(u => u.ElectionsVoted)
				   .Include(u => u.FK_IDCard)
				   .Include(u => u.FK_IDCard.CardPhotos)
				   .FirstOrDefaultAsync(u => u.UserName == egn);
				}
				else if (useNavigationalProperties)
				{
					return await userManager.Users.Include(u => u.ElectionsVoted)
						.Include(u => u.FK_IDCard)
						.FirstOrDefaultAsync(u => u.UserName == egn);
				}

				return await userManager.Users
						.FirstOrDefaultAsync(u => u.UserName == egn);//username = egn
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<List<User>> ReadAllUsersAsync(bool useNavigationalProperties = false, bool photoes = false)
		{
			try
			{
				IQueryable<User> query = userManager.Users;
				if (photoes)
				{
					query = query.Include(u => u.FK_IDCard)
						.Include(u => u.FK_IDCard.CardPhotos);
				}
				else if (useNavigationalProperties)
				{
					query = query.Include(u => u.FK_IDCard);
				}

				return await query.ToListAsync();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task UpdateUserAsync(User item, bool useNavigationalProperties = false, bool photoes = false)
		{
			try
			{
				User user = await ReadUserAsync(item.Id, useNavigationalProperties, photoes);

				if (user != null)
				{
					user.Authenticated = item.Authenticated;
					user.EligibleForVoting = item.EligibleForVoting;
					user.ElectionsVoted = item.ElectionsVoted;
					user.FK_IDCard = item.FK_IDCard;
					user.PasswordHash = item.PasswordHash;

					await userManager.UpdateAsync(user);

				}

				await context.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task DeleteUserAsync(string id)
		{
			try
			{
				User user = await ReadUserAsync(id, true, true);

				if (user == null)
				{
					throw new ArgumentException("There is no user in the database with that id!");
				}

				if (user.FK_IDCard.CardPhotos != null)
				{
					context.IDCardsPhotos.Remove(user.FK_IDCard.CardPhotos);
					await context.SaveChangesAsync();
				}

				await userManager.DeleteAsync(user);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task DeleteUserByEGNAsync(string egn)
		{
			try
			{
				User user = await ReadUserEGNAsync(egn, true, true);

				if (user == null)
				{
					throw new InvalidOperationException("User not found for deletion!");
				}
				if (user.FK_IDCard.CardPhotos != null)
				{
					context.IDCardsPhotos.Remove(user.FK_IDCard.CardPhotos);
					await context.SaveChangesAsync();
				}

				await userManager.DeleteAsync(user);
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task ChangePassword(string userid, string password)
		{
			User user = await ReadUserAsync(userid);
			await userManager.RemovePasswordAsync(user);

			await userManager.AddPasswordAsync(user, password);
			await context.SaveChangesAsync();
		}

		#endregion

		#region Find Methods

		/*    public async Task<User> FindUserByEGNAsync(string egn, bool useNavigationalProperties = false, bool photoes = false)
            {
                try
                {
                    if (photoes)
                    {
                        return await context.Users
                            .Include(u => u.FK_IDCard)
                            .Include(u => u.FK_IDCard.CardPhotos)
                            .FirstOrDefaultAsync(u => u.FK_IDCard.EGN == egn);
                    }
                    if (useNavigationalProperties)
                    {
                        return await context.Users
                            .Include(u => u.FK_IDCard)
                            .FirstOrDefaultAsync(u => u.FK_IDCard.EGN == egn);
                    }

                    return context.Users.FirstOrDefault(u => u.FK_IDCard.EGN == egn);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        */
		public async Task<bool> ExistsAsync(string id)
		{
			User user = await context.Users.FindAsync(id);

			if (user == null)
			{
				return false;
			}

			return true;
		}
		public async Task<bool> ExistByEGNAsync(string egn)
		{
			User user = await context.Users.FirstOrDefaultAsync(u => u.UserName == egn);

			if (user == null)
			{
				return false;
			}

			return true;
		}
		#endregion
	}
}
