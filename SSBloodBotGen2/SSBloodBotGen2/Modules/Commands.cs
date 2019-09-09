using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace SSBloodBotGen2.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        List<string> allMats = new List<string>(new string[] { "UMBRA SHARD", "GLOOM DUST", "VEILED CRYSTAL", "SIRENS POLLEN", "RIVERBUD", "AKUNDAS BITE", "WINTERS KISS", "SEASTALK", "ANCHOR WEED" });
       

        List<UserModel> user = new List<UserModel>();

        [Command("help")]
        public async Task help(string repeat = "")
        {
            switch (repeat.Split(' ')[0])
            {
                case "":
                    await Context.Channel.SendMessageAsync($"Greetings, {Context.User.Mention}! \nThe current active commands are as follows: \n \nhelp \nrepeatme \ndonate \nabsent");
                    await Context.Message.DeleteAsync();
                    break;
                case "help":
                    await Context.Channel.SendMessageAsync($"Greetings, {Context.User.Mention}! \n \nThis command.. Well.. You've already used it correctly? It helps with command syntax. Try using help with a different command, and for a list of all commands currently active, just use help on its own!");
                    await Context.Message.DeleteAsync();
                    break;
                case "donate":
                    await Context.Channel.SendMessageAsync($"Greetings, {Context.User.Mention}! \n \nThis command is used to track donations made to the guild, usage is very simple, just the command followed by whatever you have sent to an officer as contribution to the guilds raiding efforts!");
                    await Context.Message.DeleteAsync();
                    break;
                case "absent":
                    await Context.Channel.SendMessageAsync($"Greetings, {Context.User.Mention}! \n \nThis command is used to seamlessly report an absence from raid to the Council. Only Council members will be able to see this report, and will act accordingly. Should you wish to include a reason you're welcome to do so, though it is not mandatory. However, consistent absence without reason may result in your removal from the raid team.");
                    await Context.Message.DeleteAsync();
                    break;
                default:
                    await Context.Channel.SendMessageAsync($"Greetings, {Context.User.Mention}! \n \nIt would seem the command you want help with doesn't fully exist yet! \nFurther information can be obtained from {Context.Guild.GetUser(152561104441180161).Mention}!");
                    await Context.Message.DeleteAsync();
                    break;
            }
        }
        [Command("repeatme")]
        public async Task repeatMe([Remainder] string repeat)
        {
            await Context.Channel.SendMessageAsync(repeat);
        }
        [Command("dbtest")]
        public async Task dbTest([Remainder] string repeat = "")
        {
            if (repeat != "")
            {

            }
            else
            {
                user = SqliteDataAccess.LoadUser();
            }
        }
        [Command("donate")]
        public async Task donate([Remainder] string repeat = "")
        {

            DonationModel d = new DonationModel();

            if (SqliteDataAccess.LoadUser().Any(user => user.UID == Convert.ToInt64(Context.User.Id)))
            {

            }
            else
            {
                long i = 0;
                string str = Context.User.Mention.Split("<@!")[1].Split('>')[0];
                UserModel u = new UserModel();
                long.TryParse(str, out i);
                Console.WriteLine(str);
                Console.WriteLine(i);
                Console.WriteLine(Context.User.Mention);
                u.UID = (i);
                u.Name = Context.User.Username;
                Console.WriteLine(u.UID + u.Name);
                SqliteDataAccess.SaveUser(u);
            }

            switch (repeat.Split(' ')[0])
            {
                case "":
                    await Context.Channel.SendMessageAsync($"Donating air does not count!");
                    await Context.Message.DeleteAsync();
                    await Context.Guild.GetTextChannel(491377864575549440).SendMessageAsync($"{Context.User.Mention} just tried to donate air to the guild, the cheap nonce!");
                    break;

                default:
                    if (repeat.Contains("<@!"))
                    {
                        var target = (repeat.Split("<@!")[1].Split('>')[0]);
                        string[] str = { repeat.Split("<@!")[0], repeat.Split($"{target}>")[1] };
                        var donation = string.Join(" ", str);
                        await Context.Channel.SendMessageAsync($"Thankyou, {Context.User.Mention}, for your donation!");
                        await Context.Message.DeleteAsync();
                        await Context.Guild.GetTextChannel(491377864575549440).SendMessageAsync($"{Context.User.Mention} has submitted a donation of {donation} to {(Context.Message.MentionedUsers.First() as SocketGuildUser).Nickname}! \nPlease check your mail when you're next in-game!");
                        await Context.Message.MentionedUsers.First().SendMessageAsync($"{Context.User.Mention} has donated {donation} and has mailed it to you! Please check your in-game mail when you're next online!");
                        d.Nickname = (Context.User as SocketGuildUser).Nickname;
                        d.UID = Context.User.Mention;
                        d.Donation = donation;
                        d.Target = (Context.Message.MentionedUsers.First() as SocketGuildUser).Nickname;
                        SqliteDataAccess.SaveDonation(d);
                        break;
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"Thankyou, {Context.User.Mention}, for your donation!");
                        await Context.Message.DeleteAsync();
                        await Context.Guild.GetTextChannel(491377864575549440).SendMessageAsync($"{Context.User.Mention} has submitted a donation of {repeat} to the guild! \nPlease check your mail when you're next in-game!");
                        break;
                    } /* repeat.Split("<@!")[1].Split('>')[0] */
            }
        }
        [Command("donate-bulk")]
        public async Task donatebulk([Remainder] string repeat = "")
        {
            await Context.Channel.SendMessageAsync($"This command is under heavy development and will not be functional for some time. \n \nPlease consult !help donate for an alternate use.");
            await Context.Message.DeleteAsync();

            /* bool valid = false;
            int x = 0;

            if(SqliteDataAccess.LoadUser().Any(user => user.UID == Convert.ToInt64(Context.User.Id)))
            {
             
            }
            else
            {
                long i = 0;
                string str = Context.User.Mention.Split("<@!")[1].Split('>')[0];
                UserModel u = new UserModel();
                long.TryParse(str, out i);
                Console.WriteLine(str);
                Console.WriteLine(i);
                Console.WriteLine(Context.User.Mention);
                u.UID = (i);
                u.Name = Context.User.Username;
                Console.WriteLine(u.UID + u.Name);
                SqliteDataAccess.SaveUser(u);
            }
            if (repeat == "")
            {
                await Context.Channel.SendMessageAsync($"Invalid entry! Please consult '!help donate-bulk' for more information.");
            }
            else
            {
                

                if (repeat.Contains("[") && repeat.Contains("]"))
                {
                    

                    while (repeat.Split("]")[x] != "")
                    {
                        if(x < 1)
                        {
                            long inum = 0;
                            string num = repeat.Split(" [")[x];
                            string item = repeat.Split(" [")[x + 1].Split("]")[0];

                            if(!long.TryParse(num, out inum))
                            {
                                valid = false;
                                break;
                            }

                            if(!allMats.Contains(item.ToUpper()))
                            {
                                await Context.Channel.SendMessageAsync($"Invalid entry [{item}]! Please consult '!help donate-bulk' for more information.");
                                valid = false;
                            }
                            
                            valid = true;
                        }
                        else
                        {
                            long inum = 0;
                            string num = repeat.Split("] ")[x].Split(" [")[0];
                            string item = repeat.Split(" [")[x + 1].Split("]")[0];

                            if (!long.TryParse(num, out inum))
                            {
                                valid = false;
                                break;
                            }

                            if (!allMats.Contains(item.ToUpper()))
                            {
                                await Context.Channel.SendMessageAsync($"Invalid entry [{item}]! Please consult '!help donate-bulk' for more information.");
                                valid = false;
                                break;
                            }



                            valid = true;

                        }

                        if (valid == false)
                        {
                            await Context.Channel.SendMessageAsync($"Invalid entry! Please consult '!help donate-bulk' for more information.");
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync($"Entry Valid => Dumping to Database");


                        }

                        x = x + 1;
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"Invalid entry! Please consult '!help donate-bulk' for more information.");
                }

            }


            await Context.Message.DeleteAsync();

    */
        }
        [Command("absent")]
        public async Task absent([Remainder] string repeat = "")
        {
            await Context.Channel.SendMessageAsync($"Absence logged!");
            await Context.Message.DeleteAsync();
            if (repeat == "")
            {
                await Context.Guild.GetTextChannel(271469508445732865).SendMessageAsync($"{Context.User.Mention} has submitted an absence report but left no message.");
            }
            else
            {
                await Context.Guild.GetTextChannel(271469508445732865).SendMessageAsync($"{Context.User.Mention} has submitted an absence report: {repeat}");
            }
        }
        [Command("nani")]
        public async Task nani()
        {
            await Context.Channel.SendMessageAsync($"NANI?!?!?!?!?!?!");
            await Context.Message.DeleteAsync();
        }
    }
}