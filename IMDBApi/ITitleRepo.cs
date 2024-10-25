﻿
namespace IMDBApi
{
    public interface ITitleRepo
    {
        Title AddTitle(Title title);
        Title? Delete(string tconst);
        Title? GetTitle(string tconst);
        IEnumerable<Title> GetTitleList();
        Title? UpdateTitle(string tconst, Title title);

        IEnumerable<Title> SearchTitle(string searchTerm);
    }
}