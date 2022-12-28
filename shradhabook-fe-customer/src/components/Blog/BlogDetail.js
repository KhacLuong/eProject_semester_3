import React from 'react';
import './Blog.scss';
import imagePost from '../../assets/image/background.png'
import {AiOutlineEye, AiOutlineLike} from 'react-icons/ai'
const BlogDetail = () => {
    return (
        <div className={`rikkei_single_posts`}>
            <div className={`breadcrumb_wrap`}>
                <nav className={`woocommerce_breadcrumb`}>
                    <a>Home</a>
                    <a>List posts</a>
                    <a className={`active`}>Top 10 Books to Make It a Great Year</a>
                </nav>
            </div>
            <div className={`rikkei_content`}>
                <div className={`content-area`}>
                    <main>
                        <article>
                            <div className={`posts_avatar`}>
                                <img src={imagePost}/>
                            </div>
                            <div className={`post_content shadow-xl`}>
                                <header className={`entry_header`}>
                                    <div className={`entry_meta`}>
                                        <div className={`subject active`}>
                                            <a>JAVASCRIPT</a>
                                        </div>
                                        <div className={`posted_at`}>
                                            <a>OCTOBER 11, 2022</a>
                                        </div>
                                        <div className={`posted_by`}>
                                            <a>POSTED BY: DUCANH</a>
                                        </div>
                                    </div>
                                    <h1>Top 10 Books to Make It a Great Year</h1>
                                    <div className={`viewer`}>
                                        <div className={`liked`}>
                                            <AiOutlineLike /> 1000
                                        </div>
                                        <div className={`watched`}>
                                            <AiOutlineEye /> 1000
                                        </div>
                                    </div>
                                </header>
                                <div className={`entry_content`}>Lorem ipsum dolor sit amet, consectetur adipiscing
                                    elit. Suspendisse vitae aliquet dolor, euismod tempor dui. Fusce consectetur
                                    convallis ante, in luctus enim consectetur vel. Vestibulum ante ipsum primis in
                                    faucibus orci luctus et ultrices posuere cubilia curae; Praesent dictum sem ut lacus
                                    hendrerit malesuada. Mauris sapien nisl, malesuada vitae quam in, bibendum tempus
                                    sem. Curabitur consequat, metus at tincidunt hendrerit, dui nunc rutrum ex, pulvinar
                                    auctor risus ipsum eget est. Ut et fermentum nisl. Vestibulum ante ipsum primis in
                                    faucibus orci luctus et ultrices posuere cubilia curae; Phasellus sapien erat,
                                    dictum tincidunt maximus nec, lobortis vel nibh. Donec dictum tellus vitae eros
                                    gravida, quis dapibus urna molestie. Morbi viverra suscipit purus id efficitur.
                                    Phasellus at convallis mauris, et ornare mi. Nunc libero lectus, auctor ac placerat
                                    ut, sollicitudin in neque. Donec elementum molestie posuere. Praesent scelerisque
                                    dui diam, in pellentesque diam condimentum ut. Donec vehicula, sem nec cursus
                                    feugiat, velit diam mattis mi, blandit elementum arcu leo a velit.

                                    Nullam convallis enim sit amet orci tincidunt, quis eleifend ligula molestie.
                                    Integer a venenatis massa, et gravida urna. Cras consequat mauris lacus, non
                                    ultrices nulla lacinia eu. Etiam accumsan tellus accumsan nisl porta rhoncus sed ut
                                    orci. Quisque sodales, orci in vehicula vehicula, ligula lorem fermentum lacus, non
                                    ultricies nunc metus eu lacus. Proin maximus eros vel orci egestas, id hendrerit
                                    enim maximus. Pellentesque enim sem, porttitor sit amet eros ut, mattis varius eros.
                                    Proin vel risus varius, malesuada neque ac, molestie arcu. Morbi eget varius arcu.
                                    Cras semper tellus metus, et vehicula quam rutrum vitae. Nulla gravida volutpat
                                    velit, vitae consectetur tortor rhoncus at. Integer finibus ligula vitae est
                                    pharetra, sed ullamcorper nisi fringilla.
                                </div>
                                <aside className="entry-taxonomy"></aside>
                            </div>
                        </article>
                    </main>
                </div>
                <div className={`suggess_content`}>
                    <div className={`search_posts shadow-lg`}>
                        <input className="search__input" type="text" placeholder="Search"/>
                    </div>
                    <div className={`popular_posts shadow-lg`}>
                        <div className={`popular_title`}>
                            <h2>Popular Posts</h2>
                        </div>
                        <div className={`list_posts`}>
                            <div className={`posts`}>
                                <img src={imagePost}/>
                                <div className={`posts_details`}>
                                    <div className={`posts_title`}>Top 10 Books to Make It a Great Year</div>
                                    <div className={`posts_created_at`}>October, 2022</div>
                                </div>
                            </div>
                            <div className={`posts`}>
                                <img src={imagePost}/>
                                <div className={`posts_details`}>
                                    <div className={`posts_title`}>Top 10 Books to Make It a Great Year</div>
                                    <div className={`posts_created_at`}>October, 2022</div>
                                </div>
                            </div>
                            <div className={`posts`}>
                                <img src={imagePost}/>
                                <div className={`posts_details`}>
                                    <div className={`posts_title`}>Top 10 Books to Make It a Great Year</div>
                                    <div className={`posts_created_at`}>October, 2022</div>
                                </div>
                            </div>
                            <div className={`posts`}>
                                <img src={imagePost}/>
                                <div className={`posts_details`}>
                                    <div className={`posts_title`}>Top 10 Books to Make It a Great Year</div>
                                    <div className={`posts_created_at`}>October, 2022</div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className={`popular_tags shadow-lg`}>
                        <div className={`popular_title`}>
                            <h2>Popular tags</h2>
                        </div>
                    </div>
                </div>
            </div>
            {/*<div className={`rikkei_footer`}></div>*/}
        </div>
    );
};

export default BlogDetail;