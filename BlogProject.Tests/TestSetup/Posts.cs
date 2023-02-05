using BlogProject.Tests.TestSetup.TestDb;
using Domain.Entities;

namespace BlogProject.Tests.TestSetup
{
	public static class Posts
	{
		public static void AddPosts(this TestDbContext context)
		{
			context.Posts.AddRange(
				new Post { Title = "Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.", Text = "Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed molestie risus sed ipsum tempor, sit amet fringilla nulla faucibus. Nulla facilisi. Fusce viverra, arcu id venenatis porta, augue felis vestibulum justo, eu porta dui ipsum ac justo. Cras vestibulum, justo finibus hendrerit mollis, arcu lectus placerat quam, id tincidunt libero justo laoreet tellus. Donec id leo aliquam, mattis augue et, convallis lacus. Aliquam tincidunt commodo ipsum, in tristique augue congue eu. Maecenas rhoncus tellus et ex interdum, commodo laoreet arcu dapibus. Praesent tempor mattis lectus, malesuada accumsan diam porta sed. Cras sed augue commodo, maximus erat vel, lacinia lacus. Nullam sollicitudin dui magna, quis elementum nibh facilisis a.", CreatedDate = new DateTime(2022, 11, 12) },
				new Post { Title = "Quisque in auctor odio. Suspendisse potenti.", Text = "Quisque in auctor odio. Suspendisse potenti. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Curabitur ligula mi, posuere ut ullamcorper vel, pretium sed neque. Duis vitae quam ut lectus aliquet eleifend quis quis mauris. Vestibulum efficitur egestas ante vel hendrerit. Sed elementum diam condimentum aliquam cursus. Suspendisse egestas malesuada diam, non ornare dolor congue in. Donec mattis ex non lorem dictum elementum eu id quam. Sed dapibus justo in elit vehicula imperdiet. In vestibulum eros id efficitur interdum.", CreatedDate = new DateTime(2022, 11, 12) },
				new Post { Title = "Fusce a eros eget nulla aliquet consectetur.", Text = "Fusce a eros eget nulla aliquet consectetur. Donec hendrerit at tortor ut iaculis. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Mauris consequat turpis et gravida facilisis. In hac habitasse platea dictumst. Fusce et augue nec neque placerat vulputate. Praesent efficitur sem at turpis tempor eleifend. Curabitur viverra sed ante in venenatis. Curabitur ultrices elit at rhoncus rutrum. Integer feugiat, massa vel semper venenatis, nisl sem tempor felis, scelerisque congue mi libero sed massa.", CreatedDate = new DateTime(2022, 11, 12) }
			);
		}
	}
}
