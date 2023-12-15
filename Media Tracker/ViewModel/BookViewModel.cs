using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Media_Tracker.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Media_Tracker.ViewModel
{
    public partial class BookViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<Book> allBooks;

        [ObservableProperty]
        ObservableCollection<Book> availableBooks;

        [ObservableProperty]
        ObservableCollection<Book> upcomingBooks;

        [ObservableProperty]
        ObservableCollection<Book> completedBooks;

        [ObservableProperty]
        ObservableCollection<Book> displayedBooks;

        [ObservableProperty]
        string selectedCategory = "All Books"; // Set "All Books" as the default selected category

        public BookViewModel()
        {
            // Initializing the different types of Book Collections
            AllBooks = new ObservableCollection<Book>()
            {
                new Book { BookTitle = "To Kill a Mockingbird", Author = "Harper Lee", Pages = 281, ReleaseDate = new DateTime(1960, 7, 11), IsCompleted = false, IsReleased = true },
                new Book { BookTitle = "1984", Author = "George Orwell", Pages = 328, ReleaseDate = new DateTime(1949, 6, 8), IsCompleted = true, IsReleased = true },
                new Book { BookTitle = "The Great Gatsby", Author = "F. Scott Fitzgerald", Pages = 180, ReleaseDate = new DateTime(1925, 4, 10), IsCompleted = false, IsReleased = true },
                new Book { BookTitle = "The Hobbit", Author = "J.R.R. Tolkien", Pages = 310, ReleaseDate = new DateTime(1937, 9, 21), IsCompleted = false, IsReleased = true },
                new Book { BookTitle = "The Lord of the Rings", Author = "J.R.R. Tolkien", Pages = 1178, ReleaseDate = new DateTime(1954, 7, 29), IsCompleted = true, IsReleased = true },
                new Book { BookTitle = "Brave New World", Author = "Aldous Huxley", Pages = 311, ReleaseDate = new DateTime(1932, 1, 1), IsCompleted = true, IsReleased = true },
                // Upcoming books 
                new Book { BookTitle = "The Winds of Winter 2", Author = "George R.R. Martin", Pages = 0, ReleaseDate = new DateTime(2025, 12, 6), IsCompleted = false, IsReleased = false },
                new Book { BookTitle = "Doors of Stone 2", Author = "Patrick Rothfuss", Pages = 0, ReleaseDate = new DateTime(2024, 10, 4), IsCompleted = false, IsReleased = false },
                new Book { BookTitle = "Clean Code: A Handbook of Agile Software Craftsmanship", Author = "Robert C. Martin", Pages = 68, ReleaseDate = new DateTime(2024,05,5), IsCompleted = false, IsReleased = false }
            };

            AvailableBooks = new ObservableCollection<Book>();
            UpcomingBooks = new ObservableCollection<Book>();
            CompletedBooks = new ObservableCollection<Book>();
            DisplayedBooks = AllBooks; // Start by showing all Books, which is set to "All books" in BookViewModel init
        }

        [RelayCommand]
        void ShowBookList()
        {
            switch (SelectedCategory)
            {
                case "All Books":
                    DisplayedBooks = AllBooks;
                    break;
                case "Available Books":
                    FilterAvailableBooks();
                    DisplayedBooks = AvailableBooks;
                    break;
                case "Upcoming Books":
                    FilterUpcomingBooks();
                    DisplayedBooks = UpcomingBooks;
                    break;
                case "Completed Books":
                    FilterCompletedBooks();
                    DisplayedBooks = CompletedBooks;
                    break;
            }
        }

        private void FilterAvailableBooks()
        {
            AvailableBooks.Clear();
            foreach (var book in AllBooks)
            {
                if (book.IsReleased)
                {
                    AvailableBooks.Add(book);
                }
            }
        }

        private void FilterUpcomingBooks()
        {
            UpcomingBooks.Clear();
            foreach (var book in AllBooks)
            {
                if (!book.IsReleased)
                {
                    UpcomingBooks.Add(book);
                }
            }
        }

        private void FilterCompletedBooks()
        {
            CompletedBooks.Clear();
            foreach (var book in AllBooks)
            {
                if (book.IsCompleted)
                {
                    CompletedBooks.Add(book);
                }
            }
        }
    }
}
