using System;
using CrossCutting.Models;

namespace CrossCutting.Commom
{
	public interface IJwtManager
	{

        string GenerateToken(Employer employer);

    }
}

