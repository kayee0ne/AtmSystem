using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace DataAccess.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
        """
        drop table if exists users;
        create table users
        (
            username text primary key,
            password text not null,
            role text not null,
            balance decimal not null
        );


        drop table if exists operations;
        create table operations
        (
            username text not null,
            amount decimal not null,
            timestamp timestamp not null,
            balance decimal not null,
            balance_after_operation decimal not null,
            type text not null
        );
        """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
        """
        drop table users;
        drop table operations;
        """;
}