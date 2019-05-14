
$(() => {
    const interact = like => {
        const jokeId = $("#joke-id").data('joke-id');
        $.post('/home/InteractWithJoke', { jokeId, like });
    };

    const setLikesCount = () => {
        const jokeId = $("#joke-id").data('joke-id');
        $.get('/home/getlikescount', { jokeId }, result => {
            $("#likes-count").text(result.likes);
            $("#dislikes-count").text(result.dislikes);
        });
    };
    
    setLikesCount();

    setInterval(setLikesCount, 1000);

    $("#like").on('click', () => {
        interact(true);
        setLikesCount();
        $("#like").prop('disabled', true);
        $("#dislike").prop('disabled', false);
    });

    $("#dislike").on('click', () => {
        interact(false);
        setLikesCount();
        $("#like").prop('disabled', false);
        $("#dislike").prop('disabled', true);
    });

   
});