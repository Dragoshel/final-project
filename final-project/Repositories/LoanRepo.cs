using FinalProject.Data;
using FinalProject.Models;
using System.Data;
using Dapper;

namespace FinalProject.Repos;

public class LoanRepo : ILoanRepo
{
    private readonly Engine _engine;

    public LoanRepo(Engine engine) => _engine = engine;

    public async Task<Loan> CreateAsync(CreateLoanDto createLoanDto)
    {
        using (var con = _engine.MakeConnection())
        {
            con.Open();

            const string SP_NAME = "[dbo].[CreateLoan]";

            var loanResult = await con.QueryAsync<Loan>(SP_NAME, createLoanDto,
                commandType: CommandType.StoredProcedure);

            return loanResult.SingleOrDefault();
        }
    }

    public async Task<int> ReturnBook(Guid bookCopyBarcode)
    {
        using (var con = _engine.MakeConnection())
        {
            con.Open();

            const string SP_NAME = "[dbo].[ReturnBook]";

            return await con.ExecuteAsync(SP_NAME, new { bookCopyBarcode = bookCopyBarcode },
                commandType: CommandType.StoredProcedure);
        }
    }

    public async Task<IEnumerable<OverdueNoticeDto>> CheckOverdueLoans()
    {
        using (var con = _engine.MakeConnection())
        {
            con.Open();

            const string SP_NAME = "[dbo].[CheckOverdueLoans]";

            var overdueNoticeDto = await con.QueryAsync<OverdueNoticeDto>(SP_NAME,
                commandType: CommandType.StoredProcedure);

            return overdueNoticeDto.AsEnumerable();
        }
    }

    public async Task<InterLibrary_Loan> LoanFromLibrary(LoanFromLibraryDto loanFromLibraryDto)
    {
        using (var con = _engine.MakeConnection())
        {
            con.Open();

            const string SP_NAME = "[dbo].[CreateInterLibraryLoan]";

            var interLibraryLoan = await con.QueryAsync<InterLibrary_Loan>(SP_NAME, loanFromLibraryDto,
                commandType: CommandType.StoredProcedure);

            return interLibraryLoan.SingleOrDefault();
        }
    }
}
