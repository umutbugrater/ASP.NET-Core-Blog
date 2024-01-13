using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Models
{
	public class SeedData
	{
		public static void EnsurePopulated(IApplicationBuilder application)
		{
			Context context = application.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<Context>();

			BlogManager bm = new BlogManager(new EfBlogRepository());
			CategoryManager cm = new CategoryManager(new EfCategoryRepository());
			CommentManager commentManager = new CommentManager(new EfCommentRepository());
			MessageManager messageManager = new MessageManager(new EfMessageRepository());

			if (context.Database.GetPendingMigrations().Any())
			{
				context.Database.Migrate();
			}

			if (!cm.TGetList().Any())
			{
				cm.TAdd(new Category() { CategoryName = "Yazılım" });
				cm.TAdd(new Category() { CategoryName = "Teknoloji" });
				cm.TAdd(new Category() { CategoryName = "Film & Dizi" });
				cm.TAdd(new Category() { CategoryName = "Oyun" });
				cm.TAdd(new Category() { CategoryName = "Spor" });
				cm.TAdd(new Category() { CategoryName = "Deneme" });
			}

			if (!bm.TGetList().Any())
			{
				bm.TAdd(new Blog()
				{
					BlogTitle = "REsimli blog Eekleme",
					BlogContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer molestie, lorem eu eleifend bibendum, augue purus mollis sapien, non rhoncus eros leo in nunc. Donec a nulla vel turpis consectetur tempor ac vel justo. In hac habitasse platea dictumst. Cras nec sollicitudin eros. Nunc eu enim non turpis sagittis rhoncus consectetur id augue. Mauris dignissim neque felis. Phasellus mollis mi a pharetra cursus. Maecenas vulputate augue placerat lacus mattis, nec ornare risus sollicitudin.\r\n\r\nMauris eu pulvinar tellus, eu luctus nisl. Pellentesque suscipit mi eu varius pulvinar. Aenean vulputate, massa eget elementum finibus, ipsum arcu commodo est, ut mattis eros orci ac risus. Suspendisse ornare, massa in feugiat facilisis, eros nisl auctor lacus, laoreet tempus elit dolor eu lorem. Nunc a arcu suscipit, suscipit quam quis, semper augue.\r\n\r\nQuisque arcu nulla, convallis nec orci vel, suscipit elementum odio. Curabitur volutpat velit non diam tincidunt sodales. Nullam sapien libero, bibendum nec viverra in, iaculis ut eros.",
					BlogImage = "d82d0f5a-aef5-445e-ba1e-bafb8d9ef3a7.jpg",
					BlogCreateDate = DateTime.Now,
					CategoryID = 5,
					AppUserId = 1
				});
				bm.TAdd(new Blog()
				{
					BlogTitle = "C# ile Asenkron Metotlar",
					BlogContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer molestie, lorem eu eleifend bibendum, augue purus mollis sapien, non rhoncus eros leo in nunc. Donec a nulla vel turpis consectetur tempor ac vel justo. In hac habitasse platea dictumst. Cras nec sollicitudin eros. Nunc eu enim non turpis sagittis rhoncus consectetur id augue. Mauris dignissim neque felis. Phasellus mollis mi a pharetra cursus. Maecenas vulputate augue placerat lacus mattis, nec ornare risus sollicitudin.\r\n\r\nMauris eu pulvinar tellus, eu luctus nisl. Pellentesque suscipit mi eu varius pulvinar. Aenean vulputate, massa eget elementum finibus, ipsum arcu commodo est, ut mattis eros orci ac risus. Suspendisse ornare, massa in feugiat facilisis, eros nisl auctor lacus, laoreet tempus elit dolor eu lorem. Nunc a arcu suscipit, suscipit quam quis, semper augue.\r\n\r\nQuisque arcu nulla, convallis nec orci vel, suscipit elementum odio. Curabitur volutpat velit non diam tincidunt sodales. Nullam sapien libero, bibendum nec viverra in, iaculis ut eros.",
					BlogImage = "ee1d2bb4-db62-4adc-ba51-1f8a7b76bf3d.jpg",
					BlogCreateDate = DateTime.Now,
					CategoryID = 1,
					AppUserId = 2
				});
				bm.TAdd(new Blog()
				{
					BlogTitle = "Python ile Veri Analizi",
					BlogContent = "Python programlama dilini kullanarak veri analinizi gerçekleştirebilmek oldukça mümkün. Bunun için pekçok hazır kütüphane ve bileşen var. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer molestie, lorem eu eleifend bibendum, augue purus mollis sapien, non rhoncus eros leo in nunc. Donec a nulla vel turpis consectetur tempor ac vel justo. In hac habitasse platea dictumst. Cras nec sollicitudin eros. Nunc eu enim non turpis sagittis rhoncus consectetur id augue. Mauris dignissim neque felis. Phasellus mollis mi a pharetra cursus. Maecenas vulputate augue placerat lacus mattis, nec ornare risus sollicitudin.\r\n\r\nMauris eu pulvinar tellus, eu luctus nisl. Pellentesque suscipit mi eu varius pulvinar. Aenean vulputate, massa eget elementum finibus, ipsum arcu commodo est, ut mattis eros orci ac risus. Suspendisse ornare, massa in feugiat facilisis, eros nisl auctor lacus, laoreet tempus elit dolor eu lorem. Nunc a arcu suscipit, suscipit quam quis, semper augue.\r\n\r\nQuisque arcu nulla, convallis nec orci vel, suscipit elementum odio. Curabitur volutpat velit non diam tincidunt sodales. Nullam sapien libero, bibendum nec viverra in, iaculis ut eros.",
					BlogImage = "15eebe88-aa37-4a92-ac86-9196d47afddc.jpg",
					BlogCreateDate = DateTime.Now,
					CategoryID = 1,
					AppUserId = 1
				});
				bm.TAdd(new Blog()
				{
					BlogTitle = "Kimyager Walter White",
					BlogContent = "İzlediğim en iyi dizi olan Breaking Bad yapımı. Mutalaka izlemenizi tavsiye ediyorum. IMDB sıralaması içinde de tüm zamanların en yüksek puanına sahip dizisi olarak geçmektedir. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer molestie, lorem eu eleifend bibendum, augue purus mollis sapien, non rhoncus eros leo in nunc. Donec a nulla vel turpis consectetur tempor ac vel justo. In hac habitasse platea dictumst. Cras nec sollicitudin eros. Nunc eu enim non turpis sagittis rhoncus consectetur id augue. Mauris dignissim neque felis. Phasellus mollis mi a pharetra cursus. Maecenas vulputate augue placerat lacus mattis, nec ornare risus sollicitudin.\r\n\r\nMauris eu pulvinar tellus, eu luctus nisl. Pellentesque suscipit mi eu varius pulvinar. Aenean vulputate, massa eget elementum finibus, ipsum arcu commodo est, ut mattis eros orci ac risus. Suspendisse ornare, massa in feugiat facilisis, eros nisl auctor lacus, laoreet tempus elit dolor eu lorem. Nunc a arcu suscipit, suscipit quam quis, semper augue.\r\n\r\nQuisque arcu nulla, convallis nec orci vel, suscipit elementum odio. Curabitur volutpat velit non diam tincidunt sodales. Nullam sapien libero, bibendum nec viverra in, iaculis ut eros.",
					BlogImage = "fe641290-29e5-4053-97da-396bf3252502.jpg",
					BlogCreateDate = DateTime.Now,
					CategoryID = 3,
					AppUserId = 2
				});
				bm.TAdd(new Blog()
				{
					BlogTitle = "Into The Night Dizisi",
					BlogContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer molestie, lorem eu eleifend bibendum, augue purus mollis sapien, non rhoncus eros leo in nunc. Donec a nulla vel turpis consectetur tempor ac vel justo. In hac habitasse platea dictumst. Cras nec sollicitudin eros. Nunc eu enim non turpis sagittis rhoncus consectetur id augue. Mauris dignissim neque felis. Phasellus mollis mi a pharetra cursus. Maecenas vulputate augue placerat lacus mattis, nec ornare risus sollicitudin.\r\n\r\nMauris eu pulvinar tellus, eu luctus nisl. Pellentesque suscipit mi eu varius pulvinar. Aenean vulputate, massa eget elementum finibus, ipsum arcu commodo est, ut mattis eros orci ac risus. Suspendisse ornare, massa in feugiat facilisis, eros nisl auctor lacus, laoreet tempus elit dolor eu lorem. Nunc a arcu suscipit, suscipit quam quis, semper augue.\r\n\r\nQuisque arcu nulla, convallis nec orci vel, suscipit elementum odio. Curabitur volutpat velit non diam tincidunt sodales. Nullam sapien libero, bibendum nec viverra in, iaculis ut eros.",
					BlogImage = "cc0d3bf1-94db-4d02-853d-8ffff672e1fb.jpg",
					BlogCreateDate = DateTime.Now,
					CategoryID = 3,
					AppUserId = 1
				});

				bm.TAdd(new Blog()
				{
					BlogTitle = "Fifa 22 Ekim Sonunda çıkıyor",
					BlogContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer molestie, lorem eu eleifend bibendum, augue purus mollis sapien, non rhoncus eros leo in nunc. Donec a nulla vel turpis consectetur tempor ac vel justo. In hac habitasse platea dictumst. Cras nec sollicitudin eros. Nunc eu enim non turpis sagittis rhoncus consectetur id augue. Mauris dignissim neque felis. Phasellus mollis mi a pharetra cursus. Maecenas vulputate augue placerat lacus mattis, nec ornare risus sollicitudin.\r\n\r\nMauris eu pulvinar tellus, eu luctus nisl. Pellentesque suscipit mi eu varius pulvinar. Aenean vulputate, massa eget elementum finibus, ipsum arcu commodo est, ut mattis eros orci ac risus. Suspendisse ornare, massa in feugiat facilisis, eros nisl auctor lacus, laoreet tempus elit dolor eu lorem. Nunc a arcu suscipit, suscipit quam quis, semper augue.\r\n\r\nQuisque arcu nulla, convallis nec orci vel, suscipit elementum odio. Curabitur volutpat velit non diam tincidunt sodales. Nullam sapien libero, bibendum nec viverra in, iaculis ut eros.",
					BlogImage = "cc0d3bf1-94db-4d02-853d-8ffff672e1fb.jpg",
					BlogCreateDate = DateTime.Now,
					CategoryID = 4,
					AppUserId = 1
				});
				bm.TAdd(new Blog()
				{
					BlogTitle = "MVC Yazılım ve Algoritme Öğreniyorum",
					BlogContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer molestie, lorem eu eleifend bibendum, augue purus mollis sapien, non rhoncus eros leo in nunc. Donec a nulla vel turpis consectetur tempor ac vel justo. In hac habitasse platea dictumst. Cras nec sollicitudin eros. Nunc eu enim non turpis sagittis rhoncus consectetur id augue. Mauris dignissim neque felis. Phasellus mollis mi a pharetra cursus. Maecenas vulputate augue placerat lacus mattis, nec ornare risus sollicitudin.\r\n\r\nMauris eu pulvinar tellus, eu luctus nisl. Pellentesque suscipit mi eu varius pulvinar. Aenean vulputate, massa eget elementum finibus, ipsum arcu commodo est, ut mattis eros orci ac risus. Suspendisse ornare, massa in feugiat facilisis, eros nisl auctor lacus, laoreet tempus elit dolor eu lorem. Nunc a arcu suscipit, suscipit quam quis, semper augue.\r\n\r\nQuisque arcu nulla, convallis nec orci vel, suscipit elementum odio. Curabitur volutpat velit non diam tincidunt sodales. Nullam sapien libero, bibendum nec viverra in, iaculis ut eros.",
					BlogImage = "fe641290-29e5-4053-97da-396bf3252502.jpg",
					BlogCreateDate = DateTime.Now,
					CategoryID = 1,
					AppUserId = 2
				});
				bm.TAdd(new Blog()
				{
					BlogTitle = "teknolojiler için başlık",
					BlogContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer molestie, lorem eu eleifend bibendum, augue purus mollis sapien, non rhoncus eros leo in nunc. Donec a nulla vel turpis consectetur tempor ac vel justo. In hac habitasse platea dictumst. Cras nec sollicitudin eros. Nunc eu enim non turpis sagittis rhoncus consectetur id augue. Mauris dignissim neque felis. Phasellus mollis mi a pharetra cursus. Maecenas vulputate augue placerat lacus mattis, nec ornare risus sollicitudin.\r\n\r\nMauris eu pulvinar tellus, eu luctus nisl. Pellentesque suscipit mi eu varius pulvinar. Aenean vulputate, massa eget elementum finibus, ipsum arcu commodo est, ut mattis eros orci ac risus. Suspendisse ornare, massa in feugiat facilisis, eros nisl auctor lacus, laoreet tempus elit dolor eu lorem. Nunc a arcu suscipit, suscipit quam quis, semper augue.\r\n\r\nQuisque arcu nulla, convallis nec orci vel, suscipit elementum odio. Curabitur volutpat velit non diam tincidunt sodales. Nullam sapien libero, bibendum nec viverra in, iaculis ut eros.",
					BlogImage = "15eebe88-aa37-4a92-ac86-9196d47afddc.jpg",
					BlogCreateDate = DateTime.Now,
					CategoryID = 2,
					AppUserId = 1
				});
				bm.TAdd(new Blog()
				{
					BlogTitle = "AspNet Core 6.0 Proje",
					BlogContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer molestie, lorem eu eleifend bibendum, augue purus mollis sapien, non rhoncus eros leo in nunc. Donec a nulla vel turpis consectetur tempor ac vel justo. In hac habitasse platea dictumst. Cras nec sollicitudin eros. Nunc eu enim non turpis sagittis rhoncus consectetur id augue. Mauris dignissim neque felis. Phasellus mollis mi a pharetra cursus. Maecenas vulputate augue placerat lacus mattis, nec ornare risus sollicitudin.\r\n\r\nMauris eu pulvinar tellus, eu luctus nisl. Pellentesque suscipit mi eu varius pulvinar. Aenean vulputate, massa eget elementum finibus, ipsum arcu commodo est, ut mattis eros orci ac risus. Suspendisse ornare, massa in feugiat facilisis, eros nisl auctor lacus, laoreet tempus elit dolor eu lorem. Nunc a arcu suscipit, suscipit quam quis, semper augue.\r\n\r\nQuisque arcu nulla, convallis nec orci vel, suscipit elementum odio. Curabitur volutpat velit non diam tincidunt sodales. Nullam sapien libero, bibendum nec viverra in, iaculis ut eros.",
					BlogImage = "f28ee3ea-eff1-46bc-882a-cf1d3d5d82ef.jpg",
					BlogCreateDate = DateTime.Now,
					CategoryID = 2,
					AppUserId = 2
				});
				bm.TAdd(new Blog()
				{
					BlogTitle = "Yeni Bir İşletim Sistemi Bulundu",
					BlogContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer molestie, lorem eu eleifend bibendum, augue purus mollis sapien, non rhoncus eros leo in nunc. Donec a nulla vel turpis consectetur tempor ac vel justo. In hac habitasse platea dictumst. Cras nec sollicitudin eros. Nunc eu enim non turpis sagittis rhoncus consectetur id augue. Mauris dignissim neque felis. Phasellus mollis mi a pharetra cursus. Maecenas vulputate augue placerat lacus mattis, nec ornare risus sollicitudin.\r\n\r\nMauris eu pulvinar tellus, eu luctus nisl. Pellentesque suscipit mi eu varius pulvinar. Aenean vulputate, massa eget elementum finibus, ipsum arcu commodo est, ut mattis eros orci ac risus. Suspendisse ornare, massa in feugiat facilisis, eros nisl auctor lacus, laoreet tempus elit dolor eu lorem. Nunc a arcu suscipit, suscipit quam quis, semper augue.\r\n\r\nQuisque arcu nulla, convallis nec orci vel, suscipit elementum odio. Curabitur volutpat velit non diam tincidunt sodales. Nullam sapien libero, bibendum nec viverra in, iaculis ut eros.",
					BlogImage = "f28ee3ea-eff1-46bc-882a-cf1d3d5d82ef.jpg",
					BlogCreateDate = DateTime.Now,
					CategoryID = 2,
					AppUserId = 2
				});
				bm.TAdd(new Blog()
				{
					BlogTitle = "Geniş Aile Yeniden Ekranlarda",
					BlogContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer molestie, lorem eu eleifend bibendum, augue purus mollis sapien, non rhoncus eros leo in nunc. Donec a nulla vel turpis consectetur tempor ac vel justo. In hac habitasse platea dictumst. Cras nec sollicitudin eros. Nunc eu enim non turpis sagittis rhoncus consectetur id augue. Mauris dignissim neque felis. Phasellus mollis mi a pharetra cursus. Maecenas vulputate augue placerat lacus mattis, nec ornare risus sollicitudin.\r\n\r\nMauris eu pulvinar tellus, eu luctus nisl. Pellentesque suscipit mi eu varius pulvinar. Aenean vulputate, massa eget elementum finibus, ipsum arcu commodo est, ut mattis eros orci ac risus. Suspendisse ornare, massa in feugiat facilisis, eros nisl auctor lacus, laoreet tempus elit dolor eu lorem. Nunc a arcu suscipit, suscipit quam quis, semper augue.\r\n\r\nQuisque arcu nulla, convallis nec orci vel, suscipit elementum odio. Curabitur volutpat velit non diam tincidunt sodales. Nullam sapien libero, bibendum nec viverra in, iaculis ut eros.",
					BlogImage = "15eebe88-aa37-4a92-ac86-9196d47afddc.jpg",
					BlogCreateDate = DateTime.Now,
					CategoryID = 3,
					AppUserId = 1
				});
				bm.TAdd(new Blog()
				{
					BlogTitle = "Cs deki Oyuncular Valoranta Geçiş Yaptı",
					BlogContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer molestie, lorem eu eleifend bibendum, augue purus mollis sapien, non rhoncus eros leo in nunc. Donec a nulla vel turpis consectetur tempor ac vel justo. In hac habitasse platea dictumst. Cras nec sollicitudin eros. Nunc eu enim non turpis sagittis rhoncus consectetur id augue. Mauris dignissim neque felis. Phasellus mollis mi a pharetra cursus. Maecenas vulputate augue placerat lacus mattis, nec ornare risus sollicitudin.\r\n\r\nMauris eu pulvinar tellus, eu luctus nisl. Pellentesque suscipit mi eu varius pulvinar. Aenean vulputate, massa eget elementum finibus, ipsum arcu commodo est, ut mattis eros orci ac risus. Suspendisse ornare, massa in feugiat facilisis, eros nisl auctor lacus, laoreet tempus elit dolor eu lorem. Nunc a arcu suscipit, suscipit quam quis, semper augue.\r\n\r\nQuisque arcu nulla, convallis nec orci vel, suscipit elementum odio. Curabitur volutpat velit non diam tincidunt sodales. Nullam sapien libero, bibendum nec viverra in, iaculis ut eros.",
					BlogImage = "e49c4a6a-479e-4fd7-ba5f-b446ab41ae2a.jpg",
					BlogCreateDate = DateTime.Now,
					CategoryID = 4,
					AppUserId = 2
				});
			}

			if (!commentManager.TGetList().Any())
			{
				commentManager.TAdd(new Comment() { CommentTitle = "Deneme", CommentContent = "deneme yorum", CommentDate = DateTime.Now, BlogScore = 8, BlogID = 1, AppUserID = 1 });
				commentManager.TAdd(new Comment() { CommentTitle = "Fifa 21 kıyaslama", CommentContent = "de", CommentDate = DateTime.Now, BlogScore = 4, BlogID = 1, AppUserID = 2 });
				commentManager.TAdd(new Comment() { CommentTitle = "Film Değerlendirme", CommentContent = "Çok güzel bir diziydi", CommentDate = DateTime.Now, BlogScore = 10, BlogID = 5, AppUserID = 1 });
				commentManager.TAdd(new Comment() { CommentTitle = "Film Hakkında", CommentContent = "Çok güzel bir diziydi", CommentDate = DateTime.Now, BlogScore = 2, BlogID = 4, AppUserID = 2 });
				commentManager.TAdd(new Comment() { CommentTitle = "Eğitim Düzeyi", CommentContent = "Yeni başlayanlar için mükemmel bir dersti", CommentDate = DateTime.Now, BlogScore = 4, BlogID = 2, AppUserID = 2 });
				commentManager.TAdd(new Comment() { CommentTitle = "Eğitim", CommentContent = "Çok üstünden geçilmiş. Detaylara girilmemeiş", CommentDate = DateTime.Now, BlogScore = 9, BlogID = 2, AppUserID = 1 });
				commentManager.TAdd(new Comment() { CommentTitle = "Teknoloji", CommentContent = "teknoloji deneme yoru", CommentDate = DateTime.Now, BlogScore = 1, BlogID = 3, AppUserID = 2 });
			}

			if (!messageManager.TGetList().Any())
			{
				messageManager.TAdd(new Message { MessageSubject = "Tatil", MessageDetails = "tatil açıklaması", MessageDate = DateTime.Now, ReceiverID = 1, SenderID = 2 });
				messageManager.TAdd(new Message { MessageSubject = "Ders", MessageDetails = "ders açıklaması", MessageDate = DateTime.Now, ReceiverID = 2, SenderID = 1 });
				messageManager.TAdd(new Message { MessageSubject = "Yemek", MessageDetails = "çok güzel bir yemekti", MessageDate = DateTime.Now, ReceiverID = 1, SenderID = 2 });
				messageManager.TAdd(new Message { MessageSubject = "Deneme Sınav", MessageDetails = "Sınav sonucşarı ne zaman açıklanack?", MessageDate = DateTime.Now, ReceiverID = 2, SenderID = 1 });
			}
		}

	}
}
