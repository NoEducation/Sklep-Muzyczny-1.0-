using MvcSiteMapProvider;
using SklepMuzyczny.DAT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SklepMuzyczny.Infrastructure
{
    public class SongDetailsDynamicNodeProvider : DynamicNodeProviderBase
    {
        private ISongRepository repository;
        public SongDetailsDynamicNodeProvider(ISongRepository repo) 
        {
            repository = repo;
        }
        
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            List<DynamicNode> dynamicNodes = new List<DynamicNode>();
            var songs = repository.Context.Songs.Include("Category");
            foreach(var song in songs)
            {
                DynamicNode myNode = new DynamicNode();
                myNode.Title = song.NameSong;
                myNode.Key = "Piosenka_" + song.SongId;
                myNode.ParentKey = "Kategoria_" + song.Category.CategoryId;
                myNode.RouteValues.Add("songId", song.SongId);
                dynamicNodes.Add(myNode);
            }
            return dynamicNodes;

        }
    }
}