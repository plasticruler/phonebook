export default {
  state: {
    user: null
  },
  mutations: {
    SET_USER(state, payload) {
      state.user = payload
    }
  },
  getters: {
    user(state) {
      return state.user
    }
  },
  actions: {
    signUserUp({
      commit
    }, payload) {
      console.error('Signup removed.');
    },
    signUserIn({
      commit
    }, payload) {
      commit('setLoading', false)
      commit('SET_USER', {
        username: 'Zahir',
        email: 'dummy@dummy.co.za'
      })
    },
    AUTO_SIGNIN({
      commit
    }, payload) {
      commit('SET_USER', {
        id: payload.uid,
        name: payload.displayName,
        email: payload.email,
        photoUrl: payload.photoURL
      })
    },
    resetPasswordWithEmail({
      commit
    }, payload) {
      const {
        email
      } = payload
      commit('setLoading', true)
      auth.sendPasswordResetEmail(email)
        .then(
          () => {
            commit('setLoading', false)
            console.log('Email Sent')
          }
        )
        .catch(
          error => {
            commit('setLoading', false)
            commit('setError', error)
            console.log(error)
          }
        )
    },
    logout({
      commit
    }) {
      commit('SET_USER', null)
    }
  }
}
