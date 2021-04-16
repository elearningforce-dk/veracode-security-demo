using System.Data.Common;
using System.Data.SqlClient;

namespace VeraDemoNet.Commands
{
    public class IgnoreCommand : BlabberCommandBase,IBlabberCommand
    {
        private readonly string username;

        public IgnoreCommand(DbConnection connect, string username)
        {
            this.connect = connect;
            this.username = username;
        }

        public void Execute(string blabberUsername) 
        {
            using (var action = connect.CreateCommand())
            {
                var sqlQuery = "DELETE FROM listeners WHERE blabber=@blabber AND listener=@username";
                logger.Info(sqlQuery);

                action.CommandText = sqlQuery;
                action.Parameters.Add(new SqlParameter { ParameterName = "@blabber", Value = blabberUsername });
                action.Parameters.Add(new SqlParameter { ParameterName = "@username", Value = username });
                action.ExecuteNonQuery();
            }

            using (var sqlStatement = connect.CreateCommand())
            {
                var selectQuery = "SELECT blab_name FROM users WHERE username = @username";
                logger.Info(selectQuery);

                sqlStatement.CommandText = selectQuery;
                sqlStatement.Parameters.Add(new SqlParameter("username", blabberUsername));
                var blabName = sqlStatement.ExecuteScalar();
                                
                using (var insertCommand = connect.CreateCommand())
                {
                    var ignoringEvent = username + " is now ignoring " + blabberUsername + "(" + blabName + ")";
                    var insertQuery = "INSERT INTO users_history (blabber, event) VALUES (@username, @event)";
                    
                    logger.Info(ignoringEvent);
                    logger.Info(insertQuery);

                    insertCommand.CommandText = insertQuery;
                    insertCommand.Parameters.Add(new SqlParameter("username", username));
                    insertCommand.Parameters.Add(new SqlParameter("event", ignoringEvent));
                    insertCommand.ExecuteNonQuery();
                }
            }
        }
    }
}