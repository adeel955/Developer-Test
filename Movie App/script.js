const apiKey = 'f664cd28'; // Replace with your actual API key
const apiUrl = 'https://www.omdbapi.com/';

async function searchMovies() {
    const searchInput = document.getElementById('searchInput').value;

    if (searchInput.trim() === '') {
        alert('Please enter a movie title');
        return;
    }

    const url = `${apiUrl}?apikey=${apiKey}&s=${searchInput}`;
    try {
        const response = await fetch(url);
        const data = await response.json();

        if (data.Response === 'True') {
            displayMovies(data.Search.slice(0, 3));
        } else {
            alert('No movies found. Please try again.');
        }
    } catch (error) {
        console.error('Error fetching data:', error);
    }
}

function displayMovies(movies) {
    const movieList = document.getElementById('movieList');
    movieList.innerHTML = '';

    movies.forEach(movie => {
        const movieCard = document.createElement('div');
        movieCard.classList.add('movie-card');

        const imgSrc = movie.Poster !== 'N/A' ? movie.Poster : 'placeholder-image.jpg';
        movieCard.innerHTML = `
            <img src="${imgSrc}" alt="${movie.Title}" onclick="openMoviePage('${movie.imdbID}')">
            <div class="movie-title">${movie.Title}</div>
        `;

        movieList.appendChild(movieCard);
    });
}

function openMoviePage(imdbID) {
    window.open(`https://www.imdb.com/title/${imdbID}`, '_blank');
}
