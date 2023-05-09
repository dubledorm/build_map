
namespace BuildMap
{
    public class TargetListPresenter
    {
        private Building building;
        private int start_point_id;
        private string host_name_with_port;

        public TargetListPresenter(Building building, int start_point_id, string host_name_with_port)
        {
            this.building = building;
            this.start_point_id = start_point_id;
            this.host_name_with_port = host_name_with_port;
        }
        public string toTargetList()
        {
            string result = string.Empty;
            foreach (Target target in building.possibleTargets(start_point_id))
            {   
                result += buildItem(target);
            }
            return result;
            //http://127.0.0.1:8888/mapping/buildings/1/points/2/path?target=2
            //result += $"<option value=\"http://127.0.0.1:8888/mapping/buildings/{building}>{target.name}</option>";
        }

        private string link(Target target)
        { 
            return $"http://{host_name_with_port}/mapping/buildings/{building.id}/points/{start_point_id}/path?target_id={target.id}";
        }

        private string buildItem(Target target)
        {
            string result = $@"<div class=""row"">  
							<div class=""col-xs-12 col-sm-2 text-center"">
								<div class=""entry-meta"">
									<span id=""publish_date"">{target.name}</span>
									<span><i class=""fa fa-user""></i> <a href=""#"">{target.id}</a></span>
									<span><i class=""fa fa-comment""></i> <a href=""#"">Лекции по искуственному интеллекту</a></span>
								</div>
							</div>
							<div class=""col-xs-12 col-sm-10 blog-content"">
							  <a href=""#""><img class=""img-responsive img-blog"" src=""{RequestHandler.assets_url}/images/blog/blog{target.id + 1}.jpg"" width=""100%"" alt="""" /></a>
							  <h2><a href=""blog-item.html"">Информаториум</a></h2>
							  <h3>{target.description}</h3>
							  <a class=""btn btn-primary readmore"" href=""{link(target)}"">Сюда <i class=""fa fa-angle-right""></i></a>
							</div>
						</div>";

            return result ;
        }
    }
}
