using System;

namespace TestNinja.Mocking
{
    public interface IHousekeeperStatementSaver
    {
        string SaveStatement(int housekeeperOid, string housekeeperName, DateTime statementDate);
    }
}