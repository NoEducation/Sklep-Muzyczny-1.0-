using MvcSiteMapProvider;
using SklepMuzyczny.DAT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SklepMuzyczny.Infrastructure
{
    public class CategoryDynamicNodeProvider : DynamicNodeProviderBase
    {   
        private ISongRepository repository;
        public CategoryDynamicNodeProvider(ISongRepository repo) 
        {
            repository = repo;
        }
        
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            List<DynamicNode> dynamicNodes = new List<DynamicNode>();

            foreach (var category in repository.Categories)
            {
                DynamicNode myNode = new DynamicNode();
                myNode.Title = category.NameCategory;
                myNode.Key = "Kategoria_" + category.CategoryId;
                myNode.RouteValues.Add("categoryName", category.NameCategory);
                dynamicNodes.Add(myNode);
            }
            DynamicNode secialNode = new DynamicNode();
            secialNode.Title = "Wszystkie piosenki";
            secialNode.Key = "Kategoria_" + "All";
            secialNode.RouteValues.Add("categoryName", SklepMuzyczny.Const.ConstValues.AllSongs);
            dynamicNodes.Add(secialNode);

            return dynamicNodes;
        }
    }
}