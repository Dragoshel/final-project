﻿using FinalProject.Data;
using FinalProject.Models;
using System.Data;
using Dapper;

namespace FinalProject.Repos;

public class LoanRepo : ILoanRepo
{
    private readonly Engine _engine;

    public LoanRepo(Engine engine) => _engine = engine;
    public async Task<int> CreateAsync(int MemberCardID, int Barcode)
    {
        using (var con = _engine.MakeConnection())
        {
            con.Open();

            var asd = await con.ExecuteAsync("CreateLoan", new {MemberCardID, Barcode}, commandType: CommandType.StoredProcedure);

            return asd;
        }
        
    }
}