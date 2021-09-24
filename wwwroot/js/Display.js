//	Get the HTML elements.
const img = document.getElementById("Image");
const vid = document.getElementById("Video");

//	Get refresh rate.
const refresh = screen.refresh;

window.onload = () => {
	//	Make sure it is an array.
	session = [].concat(session);

	//	Start the session from the first Image.
	refresh_content(0);

	//	Set up a page reload action at UpdateTime.
	var epoc = new Date(update_time) - new Date();
	setTimeout(location.reload, epoc);
};

function refresh_content(index)
{
	//	Stay in bounds and get the current Image.
	index = index % session.length;
	let curr = session[index];

	//	Choose which element to manipulate.
	//	Functions DO NOT clean the src of the other element.
	if (curr.is_video)
	{
		handle_vid(curr.link);
	}
	else
	{
		handle_img(curr.link);
	}

	//	Set up self to run after {refresh} seconds.
	//	Instead of setTimeout, setInterval() can be used in window.onlad() too.
	setTimeout(refresh_content, refresh * 1000, index + 1);
}

function handle_img(link)
{
	//	Process link here, when needed.
	img.src = link;
	if ( img.classList.contains("Hidden") )		{	img.classList.remove("Hidden");	}
	if ( !img.classList.contains("Display") )	{	img.classList.add("Display");	}
	if ( vid.classList.contains("Display") )	{	img.classList.remove("Display");	}
	if ( !vid.classList.contains("Hidden") )	{	img.classList.add("Hidden");	}
}

function handle_vid(link)
{
	//	Process link here, when needed.
	vid.src = link;
	if ( vid.classList.contains("Hidden") )		{	img.classList.remove("Hidden");	}
	if ( !vid.classList.contains("Display") )	{	img.classList.add("Display");	}
	if ( img.classList.contains("Display") )	{	img.classList.remove("Display");	}
	if (!img.classList.contains("Hidden"))		{	img.classList.add("Hidden");	}
}
