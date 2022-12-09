import React from "react";

class MyComponent extends React.Component {

    state = {
        name: 'Đức Anh',
        age: 22
    };

    handleClick() {
        console.log(">> Click me")
    };
    handleOnSubmit(event) {
        event.preventDefault()
    }
    // JSX
    render() {
        return (
            <div> My name is {this.state.name}
                <div>
                    <form onSubmit={(event) => this.handleOnSubmit(event)}>
                        <input
                            type="text"
                            onChange={(event) => this.setState({name : event.target.value})}
                        />
                        <button>Submit</button>
                    </form>
                </div>
            </div>
        )
    }
}

export default MyComponent;